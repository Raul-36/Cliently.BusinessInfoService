using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Core.Businesses.Repositories.Base;

namespace Application.Businesses.Handlers
{
    public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, bool>
    {
        private readonly IBusinessRepository businessRepository;

        public DeleteBusinessCommandHandler(IBusinessRepository businessRepository)
        {
            this.businessRepository = businessRepository;
        }

        public async Task<bool> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
        {
            await businessRepository.DeleteByIdAsync(request.Id);
            return true;
        }
    }
}