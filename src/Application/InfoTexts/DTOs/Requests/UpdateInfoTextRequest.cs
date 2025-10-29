using System;

namespace Application.InfoTexts.DTOs.Requests
{
    public class UpdateInfoTextRequest
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}