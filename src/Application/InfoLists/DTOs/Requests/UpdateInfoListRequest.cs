using System;

namespace Application.InfoLists.DTOs.Requests
{
    public class UpdateInfoListRequest
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid UserId { get; set; }
    }
}