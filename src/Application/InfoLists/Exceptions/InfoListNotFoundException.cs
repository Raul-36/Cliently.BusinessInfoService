using System;

namespace Application.InfoLists.Exceptions
{
    public class InfoListNotFoundException : Exception
    {
        public InfoListNotFoundException(string message) : base(message) { }
        public InfoListNotFoundException(Guid id) : base($"InfoList with ID '{id}' was not found.") { }
    }
}
