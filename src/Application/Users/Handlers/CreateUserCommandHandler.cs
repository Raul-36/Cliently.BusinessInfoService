using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Users.Commands;
using Application.Users.DTOs.Responses;
using Core.Users.Repositories;
using Core.Users.Entities;
using AutoMapper;

namespace Application.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request.Request);
            await userRepository.AddAsync(user);
            return mapper.Map<UserResponse>(user);
        }
    }
}
