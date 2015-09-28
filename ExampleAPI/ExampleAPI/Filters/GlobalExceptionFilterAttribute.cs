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
			var controller = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;

			if (actionExecutedContext.Exception is ValidationException)
			{
				var validationException = actionExecutedContext.Exception as ValidationException;
				actionExecutedContext.Response = new ErrorModel(validationException).ToHttpResponseMessage();
				return;
			}
			else if (actionExecutedContext.Exception is ServiceLayerException)
			{
				var genericException = actionExecutedContext.Exception as ServiceLayerException;
				actionExecutedContext.Response = new ErrorModel(genericException).ToHttpResponseMessage();
				return;
			}
			else
			{
				actionExecutedContext.Response = new ErrorModel
				{
					ErrorCode = "UNKNOWN_ERROR",
					Message = actionExecutedContext.Exception.Message
				}.ToHttpResponseMessage();
				return;
			}
		}
	}
}