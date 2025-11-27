using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Users.Commands;
using Core.Users.Repositories;
namespace Application.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await userRepository.DeleteAsync(request.Id);
        }
    }
}
