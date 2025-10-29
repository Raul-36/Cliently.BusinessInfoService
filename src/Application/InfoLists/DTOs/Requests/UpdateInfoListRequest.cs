using System;

namespace Application.InfoLists.DTOs.Requests
{
    public class UpdateInfoListRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}