using MediatR;
using Application.Businesses.DTOs.Responses;
using Application.Common;

namespace Application.Businesses.Queries
{
    public record GetBusinessByIdQuery(Guid Id) : IRequest<Result<BusinessResponse>>;
}
