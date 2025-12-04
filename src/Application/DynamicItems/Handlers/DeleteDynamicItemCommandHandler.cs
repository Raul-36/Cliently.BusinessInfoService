using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Application.Users.Services.Base;
using Core.DynamicItems.Repositories.Base;
using Application.DynamicItems.Exceptions;

namespace Application.DynamicItems.Handlers
{
    public class DeleteDynamicItemCommandHandler : IRequestHandler<DeleteDynamicItemCommand>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IAccessCheckService accessCheckService;

        public DeleteDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository, IAccessCheckService accessCheckService)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.accessCheckService = accessCheckService;
        }

        public async Task Handle(DeleteDynamicItemCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToDynamicItem(request.UserId, request.Id))
            {
                throw new DynamicItemNotFoundException(request.Id);
            }
            
            await dynamicItemRepository.DeleteByIdAsync(request.Id);
        }
    }
}