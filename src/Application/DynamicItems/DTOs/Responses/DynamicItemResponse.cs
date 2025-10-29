using System;

namespace Application.DynamicItems.DTOs.Responses
{
    public class DynamicItemResponse
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public required Dictionary<string, object> Properties { get; set; }
    }
}