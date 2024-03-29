﻿using System;

namespace Ictx.WebApp.Core.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message = "") 
            : base(String.IsNullOrEmpty(message) ? "Dato non trovato." : message)
        { }
    }
}
