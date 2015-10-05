using ExampleAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleAPI.Exceptions
{
	public class ValidationException : Exception
	{
		public string ErrorCode { get; set; }
		public JObject ErrorDetails { get; set; }

		public ValidationException(JObject errorDetails) 
			: base("Validation occured, see ErrorDetails for more details.")
		{
			ErrorDetails = errorDetails;
            ErrorCode = Constants.ErrorCodes.ValidationErrors;
		}
	}
}