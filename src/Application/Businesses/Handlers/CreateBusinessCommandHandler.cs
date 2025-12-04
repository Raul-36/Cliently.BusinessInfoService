using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Application.Businesses.DTOs.Responses;
using Core.Businesses.Repositories.Base;
using Core.Businesses.Entities;
using AutoMapper;
using Core.Users.Repositories;
using Application.Businesses.Exceptions;

namespace Application.Businesses.Handlers
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, BusinessResponse>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CreateBusinessCommandHandler(IBusinessRepository businessRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.businessRepository = businessRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<BusinessResponse> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Business.UserId);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User does not exist to create a business.");
            }
            var userBuisnesses = await businessRepository.GetByUserIdAsync(request.Business.UserId);
            if (userBuisnesses != null)
            {
                throw new UserAlreadyHasBusinessException("User already has a business.");
            }
            var business = this.mapper.Map<Business>(request.Business);

            var createdBusiness = await businessRepository.AddAsync(business);

            var mappedResponse = this.mapper.Map<BusinessResponse>(createdBusiness);
            return mappedResponse;
        }
    }
}
