using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Exceptions
{
    public class LolAccountAlreadyLinkedException : Exception
    {
        public LolAccountAlreadyLinkedException(string message) : base(message) { }
    }
}
