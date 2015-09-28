using ExampleAPI.Filters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
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

			//List<string> errors = new List<string>();

			config.Formatters.JsonFormatter.SerializerSettings.Error = (object sender, ErrorEventArgs args) =>
			{
				
				//errors.Add(args.ErrorContext.Error.Message);
				args.ErrorContext.Handled = true;
			};
		}
	}
}
