using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Core.Businesses.Repositories.Base;
using AutoMapper;

namespace Application.Businesses.Handlers
{
    public class UpdateBusinessNameCommandHandler : IRequestHandler<UpdateBusinessNameCommand, string>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IMapper _mapper;

        public UpdateBusinessNameCommandHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            this.businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdateBusinessNameCommand request, CancellationToken cancellationToken)
        {
            var newName = await businessRepository.SetNameByIdAsync(request.Business.Id, request.Business.Name);
            return newName;
        }
    }
}