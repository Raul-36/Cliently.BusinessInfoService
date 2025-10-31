namespace Application.InfoLists.DTOs.Requests
{
    public class CreateInfoListRequest
    {
        public required string Name { get; set; }
        public required Guid BusinessId { get; set; }
    }
}