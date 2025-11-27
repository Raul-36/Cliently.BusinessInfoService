namespace Application.Businesses.DTOs.Requests
{
    public class UpdateBusinessRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Name { get; set; }
    }
}
