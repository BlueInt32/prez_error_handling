using ExampleAPI.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
			ErrorDetails = JObject.Parse(validationException.ErrorDetailsJson);
			Message = validationException.Message;
		}
		public ErrorModel(ServiceLayerException tApiGenericException)
		{
			ErrorCode = tApiGenericException.ErrorCode;
			Message = tApiGenericException.Message;
		}

		public string ErrorCode { get; set; }
		public JObject ErrorDetails { get; set; }
		public string Message { get; set; }
	}
}