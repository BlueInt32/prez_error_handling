using ExampleAPI.Exceptions;
using ExampleAPI.Extensions;
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