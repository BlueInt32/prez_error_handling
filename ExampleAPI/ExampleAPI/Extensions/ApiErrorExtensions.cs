using ExampleAPI.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace ExampleAPI.Extensions
{

    public static class ApiErrorExtensions
	{
		public static JObject ToTApiFormat(this ModelStateDictionary modelState)
		{
            JObject validationErrorsDescriptor = new JObject();

			List<string> propertiesInError = new List<string>();
            foreach (var modelPropertyName in modelState.Keys)
			{
				ModelState propertyState = modelState[modelPropertyName];
				foreach (var innerError in propertyState.Errors)
				{
					if (propertiesInError.Contains(modelPropertyName))
                        // this property error has already been added
                        continue; 
					string errorTypeDetail = string.IsNullOrEmpty(innerError.ErrorMessage) 
                        ? Constants.PropertyErrorType.Malformed // json.net parsing error
                        : innerError.ErrorMessage; // t1 validation

                    validationErrorsDescriptor[modelPropertyName.ExtractPropertyName()] = errorTypeDetail;
					propertiesInError.Add(modelPropertyName);
				}
			}
			return validationErrorsDescriptor;
		}
        
		private static string ExtractPropertyName(this string fullModelStateProperty)
		{
			return fullModelStateProperty.Substring(fullModelStateProperty.IndexOf('.') + 1);
		}
	}
}