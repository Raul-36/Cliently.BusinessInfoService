using MediatR;

namespace Application.Businesses.Commands
{
    public class DeleteBusinessCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}