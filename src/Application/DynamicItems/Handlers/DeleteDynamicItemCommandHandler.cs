using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Core.DynamicItems.Repositories.Base;

namespace Application.DynamicItems.Handlers
{
    public class DeleteDynamicItemCommandHandler : IRequestHandler<DeleteDynamicItemCommand, bool>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;

        public DeleteDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository)
        {
            this.dynamicItemRepository = dynamicItemRepository;
        }

        public async Task<bool> Handle(DeleteDynamicItemCommand request, CancellationToken cancellationToken)
        {
            await dynamicItemRepository.DeleteByIdAsync(request.Id);
            return true;
        }
    }
}