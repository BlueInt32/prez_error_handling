<Query Kind="Program" />

void Main()
{
	
}

// Define other methods and classes here
public class ValidationException : TApiException
{
	public string ErrorDetailsJson { get; set; }
	public ValidationException(string errorDetailsJson) : base("Validation errors occured", "VALIDATION_ERRORS")
	{
		ErrorDetailsJson = errorDetailsJson;
	}
}

public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
{
	NullValueHandling = NullValueHandling.Ignore,
	DateFormatString = "s"
};