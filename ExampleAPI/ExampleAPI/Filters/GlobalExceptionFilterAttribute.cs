using ExampleAPI.Exceptions;
using ExampleAPI.Extensions;
using ExampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace ExampleAPI.Filters
{
	public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext.Exception is ValidationException)
			{
				var validationException = actionExecutedContext.Exception as ValidationException;
				actionExecutedContext.Response = new ErrorModel(validationException).ToHttpResponseMessage();
				return;
			}
			else if (actionExecutedContext.Exception is ServiceLayerException)
			{
				var serviceLayerException = actionExecutedContext.Exception as ServiceLayerException;
				actionExecutedContext.Response = new ErrorModel(serviceLayerException).ToHttpResponseMessage();
				return;
			}
			else
			{
				actionExecutedContext.Response = new ErrorModel
				{
					ErrorCode = Constants.ErrorCodes.Unknown,
					Message = actionExecutedContext.Exception.Message
				}.ToHttpResponseMessage();
				return;
			}
		}
	}
}