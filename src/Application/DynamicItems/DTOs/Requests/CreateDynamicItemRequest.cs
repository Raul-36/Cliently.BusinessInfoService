using System;

namespace Application.DynamicItems.DTOs.Requests
{
    public class CreateDynamicItemRequest
    {
        public Guid ListId { get; set; }
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required Dictionary<string, object> Properties { get; set; }
    }
}