using System;

namespace Application.Businesses.Exceptions
{
    public class BusinessNotFoundException : Exception
    {
        public BusinessNotFoundException(string message) : base(message) { }
        public BusinessNotFoundException(Guid id) : base($"Business with ID '{id}' was not found.") { }
    }
}
