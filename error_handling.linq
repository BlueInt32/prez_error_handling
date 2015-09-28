<Query Kind="Program" />

void Main()
{

}

// Define other methods and classes here
public class TAPIExceptionFilterAttribute : ExceptionFilterAttribute
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
		else if (actionExecutedContext.Exception is TApiException)
		{
			var genericException = actionExecutedContext.Exception as TApiException;
			actionExecutedContext.Response = new ErrorModel(genericException).ToHttpResponseMessage();
			return;
		}
		else if (actionExecutedContext.Exception is RestClientException)
		{
			var graphUnhandledException = actionExecutedContext.Exception as RestClientException;
			actionExecutedContext.Response = new ErrorModel(graphUnhandledException).ToHttpResponseMessage();
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

public class ErrorModel
{
	public ErrorModel()
	{

	}
	public ErrorModel(ValidationException tApiValidationException )
	{
		ErrorCode = tApiValidationException.ErrorCode;
		ErrorDetails = JObject.Parse(tApiValidationException.ErrorDetailsJson);
		Message = tApiValidationException.Message;
	}
	public ErrorModel(TApiException tApiGenericException)
	{
		ErrorCode = tApiGenericException.ErrorCode;
		Message = tApiGenericException.Message;
	}
	public ErrorModel(RestClientException graphUnhandledException)
	{
		ErrorCode = "UNKNOWN_ERROR";
		Message = graphUnhandledException.Message;
	}

	public string ErrorCode { get; set; }
	public JObject ErrorDetails { get; set; }
	public string Message { get; set; }		
}

public static void Register(HttpConfiguration config)
{
	config.MapHttpAttributeRoutes();
	
	#region Search
	config.Routes.MapHttpRoute(
		name: "SearchProductsFullTenant",
		routeTemplate: "api/products/fulltenantsearch",
		defaults: new
		{
			controller = "SearchProductsFullTenant"
		}
	);
	config.Routes.MapHttpRoute(
		name: "SearchSupplyChainsSimple",
		routeTemplate: "api/searchproducts",
		defaults: new
		{
			controller = "SearchSupplyChainsSimple"
		}
	);

	config.Routes.MapHttpRoute(
		name: "FullSuppliedSourcesSearchApi",
		routeTemplate: "api/sources/fulltenantsearch",
		defaults: new
		{
			controller = "SearchSuppliedSourcesFullTenant"
		}
	);
	#endregion

	config.Routes.MapHttpRoute(
		name: "DefaultApi",
		routeTemplate: "api/{controller}/{id}",
		defaults: new { id = RouteParameter.Optional }
	);

	config.MessageHandlers.Add(new AuthenticationHandler());

	config.Filters.Add(new TAPIExceptionFilterAttribute());

	config.Formatters.JsonFormatter.SerializerSettings = JsonSerializerSettings;

	var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
	config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
	
	var jsonFormatter = config.Formatters.JsonFormatter;
}


public static class ApiErrorExtensions
{
	public static HttpResponseMessage ToHttpResponseMessage(this ErrorModel errorModel)
	{
		var json = JsonConvert.SerializeObject(errorModel, WebApiConfig.JsonSerializerSettings);

		StringContent stringContent = new StringContent(json);
		stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

		HttpResponseMessage httpResponseMessage = new HttpResponseMessage
		{
			Content = stringContent,
			StatusCode = ResolveHttpStatus(errorModel.ErrorCode)
		};

		return httpResponseMessage;
	}

	private static HttpStatusCode ResolveHttpStatus(string errorCode)
	{
		if (string.IsNullOrWhiteSpace(errorCode))
			return (HttpStatusCode)200;
		else
		{
			if (!ConfiguredCodes.Any(c => c.Code == errorCode))
				return (HttpStatusCode)500;
			return (HttpStatusCode)Convert.ToInt32(ConfiguredCodes.FirstOrDefault(c => c.Code == errorCode).HttpStatus);
		}
	}

	private static IEnumerable<ErrorCodeConfigElement> ConfiguredCodes
	{
		get { return ErrorCodesList.CodesList; }
	}
}


/// <summary>
/// Create new product ref
/// <a href='http://wiki.t1.corp/display/TRAN/Create+New+Product+Ref' target='_blank'> - Confluence Page</a>
/// </summary>
/// <param name="productRefModel">Product ref to be created</param>
[Route(""), ValidateModel,
SwaggerResponse(HttpStatusCode.OK, Type = typeof(ProductRefFullDTO)),
SwaggerResponseExample(typeof(ProductRefFullDTO), typeof(ProductRefExample))]
public HttpResponseMessage Post([FromBody]ProductRefModel productRefModel)
{
	var service = new TApiProductRefService(Context); 
	var productRefDto = service.CreateProductRef(this, productRefModel.ToArgs());
	return Request.CreateResponse<ProductRefFullDTO>(HttpStatusCode.Created, productRefDto);
}



public class ValidateModelAttribute : ActionFilterAttribute
{
	private ILogger _logger;
	public ValidateModelAttribute()
	{
		if (LoggerManager.LoggerProvider != null)
		{
			_logger = LoggerManager.LoggerProvider.GetLogger(GetType());
		}
	}
	public override void OnActionExecuting(HttpActionContext actionContext)
	{
		if (actionContext.ModelState.IsValid == false)
		{
			var errorDetailsJson = actionContext.ModelState.ToTApiFormat();
			_logger.Debug(errorDetailsJson);
			throw new ValidationException(errorDetailsJson);

		}
	}
}

public static class ModelErrorCollectionExtensions
{
	public static string ToTApiFormat(this ModelStateDictionary modelState)
	{
		List<string> jsonErrors = new List<string>();
		List<string> propertiesInError = new List<string>();
		foreach (var item in modelState.Keys)
		{
			ModelState m = modelState[item];
			foreach (var innerError in m.Errors)
			{
				if (propertiesInError.Contains(item))
					continue; // this property has already been converted to json
				if (!item.Contains('.'))
					continue;
				string errorTypeDetail = string.Empty;
				if (innerError.Exception is JsonSerializationException)
					errorTypeDetail = "MALFORMED";
				else
					errorTypeDetail = innerError.ErrorMessage;
				jsonErrors.Add(string.Format("'{0}':'{1}'", item.ExtractPropertyName(), errorTypeDetail));
				propertiesInError.Add(item);
			}
		}
		return string.Concat("{", string.Join(",", jsonErrors), "}");
	}
	private static string ExtractPropertyName(this string fullModelStateProperty)
	{
		return fullModelStateProperty.Split('.')[1];
	}
}

public class TApiRequiredAttribute : RequiredAttribute
{
	public TApiRequiredAttribute()
	{
		this.ErrorMessage = "MISSING";
	}
}

public class TApiStringLength : StringLengthAttribute
{
	public TApiStringLength(int maximumLength)
		: base(maximumLength)
	{
		this.ErrorMessage = "TOO_LONG";
	}
}
	
public class ExistingVisibilityLevelAttribute : ValidationAttribute
{
	public override bool IsValid(object value)
	{
		var requestType = value as string;
		VisibilityLevel enumValue;
		return Enum.TryParse<VisibilityLevel>(requestType, out enumValue);
	}
}