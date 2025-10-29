using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Queries;
using Core.Businesses.Repositories.Base;

namespace Application.Businesses.Handlers
{
    public class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, BusinessResponse>
    {
        private readonly IBusinessRepository businessRepository;

        public GetBusinessByIdQueryHandler(IBusinessRepository businessRepository)
        {
            this.businessRepository = businessRepository;
        }

        public async Task<BusinessResponse> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new BusinessResponse { Id = request.Id, Name = "Dummy Business" });
        }
    }
}
