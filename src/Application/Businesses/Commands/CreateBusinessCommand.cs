using MediatR;
using Application.Businesses.DTOs.Requests;
using Application.Businesses.DTOs.Responses;

namespace Application.Businesses.Commands
{
    public record CreateBusinessCommand(CreateBusinessRequest Business) : IRequest<BusinessResponse>;
}
