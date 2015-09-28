using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleAPI.Exceptions
{
	public class ValidationException : Exception
	{
		public string ErrorCode { get; set; }
		public string ErrorDetailsJson { get; set; }

		public ValidationException(string errorDetailsJson) 
			: base("Validation occured, see ErrorDetails for more details.")
		{
			ErrorDetailsJson = errorDetailsJson;
            ErrorCode = "VALIDATION_ERRORS";
		}
	}
}