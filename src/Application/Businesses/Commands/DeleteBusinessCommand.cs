using MediatR;

namespace Application.Businesses.Commands
{
    public class DeleteBusinessCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}