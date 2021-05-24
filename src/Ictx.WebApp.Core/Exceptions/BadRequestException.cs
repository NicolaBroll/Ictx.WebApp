using System;
using System.Collections.Generic;

namespace Ictx.WebApp.Core.Exceptions
{   
    public class BadRequestException : Exception
    {  
        public Dictionary<string, IEnumerable<string>> Errors { get; }

        public BadRequestException(string message = "", Dictionary<string, IEnumerable<string>>  errors = default) : base(String.IsNullOrEmpty(message) ? "I dati inseriti non sono corretti." : message)
        {
            this.Errors = errors;
        }
    }
}
