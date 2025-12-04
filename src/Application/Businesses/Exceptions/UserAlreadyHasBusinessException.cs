using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Businesses.Exceptions
{
    public class UserAlreadyHasBusinessException : Exception
    {
        public UserAlreadyHasBusinessException(string message) : base(message) { }
    }

}