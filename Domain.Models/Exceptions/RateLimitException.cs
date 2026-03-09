using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Exceptions
{
    public class RateLimitException : Exception
    {
        public RateLimitException(string message) : base(message) { }
    }
}
