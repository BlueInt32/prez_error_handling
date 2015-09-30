using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleAPI.Models
{
    public class Constants
    {
        public static class ErrorCodes
        {
            public const string Unknown = "UNKNOWN";
            public const string ValidationErrors = "VALIDATION_ERROR";
        }
        public static class PropertyErrorType
        {
            public const string Malformed = "MALFORMED";
            public const string ForbiddenValue = "FORBIDDEN_VALUE";
            public const string TooLong = "TOO_LONG";
            public const string Missing = "MISSING";
        }
    }
}