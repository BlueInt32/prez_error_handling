using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleAPI.Exceptions
{
	public class ScenarioException : Exception
	{
		public string ErrorCode { get; set; }

		public ScenarioException(string errorCode, string message) 
            : base(message)
		{
			ErrorCode = errorCode;
        }
	}
}