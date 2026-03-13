using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Exceptions
{
    public class UserNotLoggedInException : Exception
    {
        public UserNotLoggedInException(string message) : base(message) { }
    }
}
