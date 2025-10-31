using MediatR;
using Application.Businesses.DTOs.Requests;
using Application.Businesses.DTOs.Responses;
using Application.Common;

namespace Application.Businesses.Commands
{
    public record CreateBusinessCommand(CreateBusinessRequest Business) : IRequest<Result<BusinessResponse>>;
}
