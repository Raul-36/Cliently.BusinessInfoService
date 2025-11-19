using System;

namespace Application.DynamicItems.Exceptions
{
    public class DynamicItemNotFoundException : Exception
    {
        public DynamicItemNotFoundException(string message) : base(message) { }
        public DynamicItemNotFoundException(Guid id) : base($"DynamicItem with ID '{id}' was not found.") { }
    }
}
