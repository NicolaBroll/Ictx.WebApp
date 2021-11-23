using System;
using System.Collections.Generic;

namespace Ictx.WebApp.Core.Exceptions
{   
    public class BadRequestException : Exception
    {  
        public Dictionary<string, IEnumerable<string>> Errors { get; }

        public BadRequestException(Dictionary<string, IEnumerable<string>> errors = default) : base("Validation error.")
        {
            this.Errors = errors;
        }
    }
}
