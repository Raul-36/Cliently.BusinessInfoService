using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Exceptions;
using Application.Businesses.Queries;
using AutoMapper;
using Core.Businesses.Repositories.Base;
using MediatR;

namespace Application.Businesses.Handlers
{
    public class GetBusinessByUserIdHandler : IRequestHandler<GetBusinessByUserIdQuery, BusinessResponse>
    {

        private readonly IBusinessRepository businessRepository;
        private readonly IMapper mapper;

        public GetBusinessByUserIdHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            this.businessRepository = businessRepository;
            this.mapper = mapper;
        }

        public async Task<BusinessResponse> Handle(GetBusinessByUserIdQuery request, CancellationToken cancellationToken)
        {
            var business = await businessRepository.GetByUserIdAsync(request.Id);

            if (business == null)
                throw new BusinessNotFoundException(request.Id);

            var mappedResponse = mapper.Map<BusinessResponse>(business);
            return mappedResponse;
        }
    }
}