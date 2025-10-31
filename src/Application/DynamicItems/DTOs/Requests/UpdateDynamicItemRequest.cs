using System;

namespace Application.DynamicItems.DTOs.Requests
{
    public class UpdateDynamicItemRequest
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required Dictionary<string, object> Properties { get; set; }
    }
}