using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Core.DynamicItems.Repositories.Base;
using Application.Common;

namespace Application.DynamicItems.Handlers
{
    public class DeleteDynamicItemCommandHandler : IRequestHandler<DeleteDynamicItemCommand, Result<bool>>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;

        public DeleteDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository)
        {
            this.dynamicItemRepository = dynamicItemRepository;
        }

        public async Task<Result<bool>> Handle(DeleteDynamicItemCommand request, CancellationToken cancellationToken)
        {
            await dynamicItemRepository.DeleteByIdAsync(request.Id);
            return Result<bool>.Success(true);
        }
    }
}