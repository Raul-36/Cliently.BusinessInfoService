namespace Application.Businesses.DTOs.Requests
{
    public class CreateBusinessRequest
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
    }
}
