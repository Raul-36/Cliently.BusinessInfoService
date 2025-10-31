using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Businesses.DTOs.Responses;
using Application.Businesses.Queries;
using AutoMapper;
using Core.Businesses.Repositories.Base;
using MediatR;

namespace Application.Businesses.Handlers
{
    public class GetAllBusinessesQueryHandler : IRequestHandler<GetAllBusinessesQuery, IEnumerable<ShortBusinessResponse>>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IMapper mapper;
        public GetAllBusinessesQueryHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            this.businessRepository = businessRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ShortBusinessResponse>> Handle(GetAllBusinessesQuery request, CancellationToken cancellationToken)
        {
            var businesses = await businessRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ShortBusinessResponse>>(businesses);
        }
    }
}