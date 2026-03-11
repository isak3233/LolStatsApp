using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Exceptions
{
    public class UsernameExistException : Exception
    {
        public UsernameExistException(string message) : base(message) { }
    }
}
