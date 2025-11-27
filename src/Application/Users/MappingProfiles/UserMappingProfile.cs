using AutoMapper;
using Application.Users.DTOs.Requests;
using Application.Users.DTOs.Responses;
using Core.Users.Entities;
using Application.Users.Events;
using Application.Users.Commands;

namespace Application.Users.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserCreatedEvent, CreateUserRequest>();
        }
    }
}
