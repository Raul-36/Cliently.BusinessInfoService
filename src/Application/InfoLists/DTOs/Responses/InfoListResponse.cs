using System;

namespace Application.InfoLists.DTOs.Responses
{
    public class InfoListResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}