using System;

namespace Application.InfoTexts.DTOs.Requests
{
    public class UpdateInfoTextRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Text { get; set; }
        public Guid BusinessId { get; set; }
    }
}