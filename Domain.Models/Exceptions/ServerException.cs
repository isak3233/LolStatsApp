using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.Domain.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message) { }
    }
}
