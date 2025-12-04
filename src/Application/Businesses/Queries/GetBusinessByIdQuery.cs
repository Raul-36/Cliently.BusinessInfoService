using MediatR;
using Application.Businesses.DTOs.Responses;

namespace Application.Businesses.Queries
{
    public class GetBusinessByIdQuery : IRequest<BusinessResponse>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
