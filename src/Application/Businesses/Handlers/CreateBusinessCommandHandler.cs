using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Application.Businesses.DTOs.Responses;
using Core.Businesses.Repositories.Base;
using Core.Businesses.Models;
using AutoMapper;

namespace Application.Businesses.Handlers
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, BusinessResponse>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IMapper _mapper;

        public CreateBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            this.businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<BusinessResponse> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var business = _mapper.Map<Business>(request.Business);

            var createdBusiness = await businessRepository.AddAsync(business);

            return _mapper.Map<BusinessResponse>(createdBusiness);
        }
    }
}
