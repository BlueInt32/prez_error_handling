using ExampleAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ExampleAPI.Extensions
{

	public static class ApiErrorExtensions
	{
		public static HttpResponseMessage ToHttpResponseMessage(this ErrorModel errorModel)
		{
			var json = JsonConvert.SerializeObject(errorModel);//, WebApiConfig.JsonSerializerSettings);

			StringContent stringContent = new StringContent(json);
			stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				Content = stringContent,
				StatusCode = ResolveHttpStatus(errorModel.ErrorCode)
			};

			return httpResponseMessage;
		}

		public static string ToTApiFormat(this ModelStateDictionary modelState)
		{
			List<string> jsonErrors = new List<string>();
			List<string> propertiesInError = new List<string>();
			foreach (var item in modelState.Keys)
			{
				ModelState m = modelState[item];
				foreach (var innerError in m.Errors)
				{
					if (propertiesInError.Contains(item))
						continue; // this property has already been converted to json
					if (!item.Contains('.'))
						continue;
					string errorTypeDetail = string.Empty;
					if (innerError.Exception is Exception)
						errorTypeDetail = "MALFORMED";
					else
						errorTypeDetail = innerError.ErrorMessage;
					jsonErrors.Add(string.Format("'{0}':'{1}'", item.ExtractPropertyName(), errorTypeDetail));
					propertiesInError.Add(item);
				}
			}
			return string.Concat("{", string.Join(",", jsonErrors), "}");
		}
		private static string ExtractPropertyName(this string fullModelStateProperty)
		{
			return fullModelStateProperty.Split('.')[1];
		}

		private static HttpStatusCode ResolveHttpStatus(string errorCode)
		{
			if (string.IsNullOrWhiteSpace(errorCode))
				return (HttpStatusCode)200;
			else
			{
				if (!ConfiguredCodes.Any(c => c.Code == errorCode))
					return (HttpStatusCode)500;
				return (HttpStatusCode)Convert.ToInt32(ConfiguredCodes.FirstOrDefault(c => c.Code == errorCode).HttpStatus);
			}
		}

		private static IEnumerable<ErrorCodeConfigElement> ConfiguredCodes
		{
			get { return ErrorCodesList.CodesList; }
		}
	}
}