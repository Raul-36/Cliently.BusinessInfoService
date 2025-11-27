using MediatR;

namespace Application.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
