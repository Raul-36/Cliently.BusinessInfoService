namespace Application.InfoTexts.DTOs.Requests
{
    public class CreateInfoTextRequest
    {
        public required string Name { get; set; }
        public required string Text { get; set; }
    }
}