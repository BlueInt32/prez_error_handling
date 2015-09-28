using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleAPI.Exceptions
{
	public class ServiceLayerException : Exception
	{
		public string ErrorCode { get; set; }
		public ServiceLayerException(string errorCode, string message) : base(message)
		{
			ErrorCode = errorCode;
        }
	}
}