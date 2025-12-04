using MediatR;
using Application.Users.DTOs.Requests;
using Application.Users.DTOs.Responses;

namespace Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public required CreateUserRequest CreateUser { get; set; }
    }
}
