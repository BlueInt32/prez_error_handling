using ExampleAPI.Models;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ExampleAPI.ValidationAttributes
{
    public class T1RequiredAttribute : RequiredAttribute
    {
        public T1RequiredAttribute()
        {
            ErrorMessage = Constants.PropertyErrorType.Missing;
        }
    }

    public class T1StringLength : StringLengthAttribute
    {
        public T1StringLength(int maximumLength)
            : base(maximumLength)
        {
            ErrorMessage = Constants.PropertyErrorType.TooLong;
        }
    }
    
    public class T1DecimalNotNullOrZeroAttribute : ValidationAttribute
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


    public class T1ListNotNullOrEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var nullableDecimal = value as IList;
            if (nullableDecimal == null)
                return new ValidationResult(Constants.PropertyErrorType.Missing);

            if (nullableDecimal.Count == 0)
                return new ValidationResult(Constants.PropertyErrorType.ForbiddenValue);
            return ValidationResult.Success;
        }
    }
}