using MediatR;
using Application.Businesses.DTOs.Responses;

namespace Application.Businesses.Queries
{
    public record GetBusinessByIdQuery(Guid Id) : IRequest<BusinessResponse>;
}
