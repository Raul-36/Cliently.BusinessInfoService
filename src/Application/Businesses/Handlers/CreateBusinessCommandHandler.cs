using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Application.Businesses.DTOs.Responses;
using Core.Businesses.Repositories.Base;
using Core.Businesses.Models; 

namespace Application.Businesses.Handlers
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, BusinessResponse>
    {
        private readonly IBusinessRepository businessRepository;

        public CreateBusinessCommandHandler(IBusinessRepository businessRepository)
        {
            this.businessRepository = businessRepository;
        }

        public async Task<BusinessResponse> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var business = new Business { Name = request.Business.Name };

            var createdBusiness = await businessRepository.AddAsync(business);

            return new BusinessResponse
            {
                Id = createdBusiness.Id,
                Name = createdBusiness.Name
            };
        }
    }
}
