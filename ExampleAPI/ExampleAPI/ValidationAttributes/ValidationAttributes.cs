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
        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext)
        {
            var nullableDecimal = value as decimal?;
            if(nullableDecimal == null)
                return new ValidationResult(Constants.PropertyErrorType.Missing);
            
            if (nullableDecimal == 0)
                return new ValidationResult(Constants.PropertyErrorType.ForbiddenValue);
            return ValidationResult.Success;
        }
    }
}