using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Queries;
using Core.Businesses.Repositories.Base;
using AutoMapper;
using Application.Businesses.Exceptions;

namespace Application.Businesses.Handlers
{
    public class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, BusinessResponse>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IMapper mapper;

        public GetBusinessByIdQueryHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            this.businessRepository = businessRepository;
            this.mapper = mapper;
        }

        public async Task<BusinessResponse> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
        {
            var business = await businessRepository.GetByIdAsync(request.Id);
            if (business == null)
            {
                throw new BusinessNotFoundException(request.Id);
            }

            var mappedResponse = this.mapper.Map<BusinessResponse>(business);
            return mappedResponse;
        }
    }
}
