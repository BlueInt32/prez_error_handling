using ExampleAPI.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ExampleAPI
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Filters.Add(new GlobalExceptionFilterAttribute());

            var jsonformatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = ApiJsonSerializerSettings
            };

            config.Formatters.RemoveAt(0);
            config.Formatters.Insert(0, jsonformatter);

            //config.Formatters.JsonFormatter.SerializerSettings = ApiJsonSerializerSettings;

            config.Formatters.JsonFormatter.SerializerSettings.Error = (object sender, ErrorEventArgs args) =>
			{
				
				//errors.Add(args.ErrorContext.Error.Message);
				args.ErrorContext.Handled = true;
			};
		}
        public static readonly JsonSerializerSettings ApiJsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatString = "s"
        };
    }
}
