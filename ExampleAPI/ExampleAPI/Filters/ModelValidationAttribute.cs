using ExampleAPI.Exceptions;
using ExampleAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ExampleAPI.Filters
{
	public class ModelValidationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (!actionContext.ModelState.IsValid)
			{
				var errorDetailsJson = actionContext.ModelState.ToTApiFormat();
				throw new ValidationException(errorDetailsJson);
			}
		}
	}

}