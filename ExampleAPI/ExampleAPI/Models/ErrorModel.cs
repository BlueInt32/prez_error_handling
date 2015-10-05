
using ExampleAPI.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ExampleAPI.Models
{
    public class ErrorModel
	{
        public ErrorModel()
		{

		}
		public ErrorModel(ValidationException validationException)
		{
			ErrorCode = validationException.ErrorCode;
			ErrorDetails = validationException.ErrorDetails;
			ErrorMessage = validationException.Message;
		}
		public ErrorModel(ScenarioException tApiGenericException)
		{
			ErrorCode = tApiGenericException.ErrorCode;
			ErrorMessage = tApiGenericException.Message;
		}
        
        public string ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
        public JObject ErrorDetails { get; set; }

        public HttpResponseMessage ToHttpResponseMessage()
        {
            var json = JsonConvert.SerializeObject(this, WebApiConfig.ApiJsonSerializerSettings);

            StringContent stringContent = new StringContent(json);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage
            {
                Content = stringContent,
                StatusCode = ResolveHttpStatus(ErrorCode)
            };

            return httpResponseMessage;
        }

        private static HttpStatusCode ResolveHttpStatus(string errorCode)
        {
            if (string.IsNullOrWhiteSpace(errorCode))
                return (HttpStatusCode)200;
            else
            {
                if (!ConfiguredCodes.Any(c => c.Code == errorCode))
                    return (HttpStatusCode)500;
                return (HttpStatusCode)Convert.ToInt32(
                    ConfiguredCodes
                    .FirstOrDefault(c => c.Code == errorCode).HttpStatus
                );
            }
        }

        private static IEnumerable<ErrorCodeConfigElement> ConfiguredCodes
        {
            get { return ErrorCodesList.CodesList; }
        }
    }
}