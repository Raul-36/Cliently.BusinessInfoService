using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Core.Businesses.Repositories.Base;
using AutoMapper;
using Application.Common;

namespace Application.Businesses.Handlers
{
    public class UpdateBusinessNameCommandHandler : IRequestHandler<UpdateBusinessNameCommand, Result<string>>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IMapper mapper;

        public UpdateBusinessNameCommandHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            this.businessRepository = businessRepository;
            this.mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateBusinessNameCommand request, CancellationToken cancellationToken)
        {
            var newName = await businessRepository.SetNameByIdAsync(request.Business.Id, request.Business.Name);
            if (newName == null)
            {
                return Result<string>.Failure("Business not found.");
            }
            return Result<string>.Success(newName);
        }
    }
}