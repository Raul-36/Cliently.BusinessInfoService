using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Application.Businesses.DTOs.Responses;
using Core.Businesses.Repositories.Base;
using Core.Businesses.Models;
using AutoMapper;
using Application.Common;

namespace Application.Businesses.Handlers
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, Result<BusinessResponse>>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IMapper mapper;

        public CreateBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            this.businessRepository = businessRepository;
            this.mapper = mapper;
        }

        public async Task<Result<BusinessResponse>> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var business = this.mapper.Map<Business>(request.Business);

            var createdBusiness = await businessRepository.AddAsync(business);

            var mappedResponse = this.mapper.Map<BusinessResponse>(createdBusiness);
            return Result<BusinessResponse>.Success(mappedResponse);
        }
    }
}
