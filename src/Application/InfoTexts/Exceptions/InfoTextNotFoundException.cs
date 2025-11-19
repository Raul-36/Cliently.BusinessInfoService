using System;

namespace Application.InfoTexts.Exceptions
{
    public class InfoTextNotFoundException : Exception
    {
        public InfoTextNotFoundException(string message) : base(message) { }
        public InfoTextNotFoundException(Guid id) : base($"InfoText with ID '{id}' was not found.") { }
    }
}
