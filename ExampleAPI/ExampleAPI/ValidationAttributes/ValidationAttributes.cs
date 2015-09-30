using ExampleAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleAPI.ValidationAttributes
{
    public class TApiRequiredAttribute : RequiredAttribute
    {
        public TApiRequiredAttribute()
        {
            ErrorMessage = Constants.PropertyErrorType.Missing;
        }
    }

    public class TApiStringLength : StringLengthAttribute
    {
        public TApiStringLength(int maximumLength)
            : base(maximumLength)
        {
            ErrorMessage = Constants.PropertyErrorType.TooLong;
        }
    }
    
    public class DecimalNotNullOrZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            decimal netContent = Convert.ToDecimal(value);

            if (netContent == 0)
                return new ValidationResult(Constants.PropertyErrorType.ForbiddenValue);
            return ValidationResult.Success;
        }
    }
}