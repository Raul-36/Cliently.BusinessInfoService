using System;

namespace Application.InfoLists.DTOs.Requests
{
    public class UpdateInfoListNameRequest
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}