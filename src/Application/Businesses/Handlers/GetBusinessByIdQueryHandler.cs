using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Queries;
using Core.Businesses.Repositories.Base;
using AutoMapper;
using Application.Businesses.Exceptions;
using Application.Users.Services.Base;
using System;

namespace Application.Businesses.Handlers
{
    public class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, BusinessResponse>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public GetBusinessByIdQueryHandler(IBusinessRepository businessRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.businessRepository = businessRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }
        
        public async Task<BusinessResponse> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToBusiness(request.UserId, request.Id))
            {
                throw new BusinessNotFoundException(request.Id);
            }
            
            var business = await businessRepository.GetByIdAsync(request.Id);

            var mappedResponse = mapper.Map<BusinessResponse>(business);
            return mappedResponse;
        }
    }
}
