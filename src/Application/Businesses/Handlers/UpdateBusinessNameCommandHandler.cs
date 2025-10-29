using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Core.Businesses.Repositories.Base;

namespace Application.Businesses.Handlers
{
    public class UpdateBusinessNameCommandHandler : IRequestHandler<UpdateBusinessNameCommand, string>
    {
        private readonly IBusinessRepository businessRepository;

        public UpdateBusinessNameCommandHandler(IBusinessRepository businessRepository)
        {
            this.businessRepository = businessRepository;
        }

        public async Task<string> Handle(UpdateBusinessNameCommand request, CancellationToken cancellationToken)
        {
            var newName = await businessRepository.SetNameByIdAsync(request.Id, request.NewName);
            return newName;
        }
    }
}