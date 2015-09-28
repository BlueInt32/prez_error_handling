using ExampleAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExampleAPI.ValidationAttributes
{
	public class TApiRequiredAttribute : RequiredAttribute
	{
		public TApiRequiredAttribute()
		{
			ErrorMessage = "MISSING";
		}
	}

	public class TApiStringLength : StringLengthAttribute
	{
		public TApiStringLength(int maximumLength)
			: base(maximumLength)
		{
			ErrorMessage = "TOO_LONG";
		}
	}

	public class ExistingVisibilityLevelAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			var requestType = value.ToString();
			VisibilityLevel enumValue;
			return Enum.TryParse<VisibilityLevel>(requestType, out enumValue);
		}
	}
}