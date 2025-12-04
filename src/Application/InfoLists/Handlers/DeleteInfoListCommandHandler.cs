using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Application.Users.Services.Base;
using Core.InfoLists.Repositories.Base;
using Application.InfoLists.Exceptions;

namespace Application.InfoLists.Handlers
{
    public class DeleteInfoListCommandHandler : IRequestHandler<DeleteInfoListCommand>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IAccessCheckService accessCheckService;

        public DeleteInfoListCommandHandler(IInfoListRepository infoListRepository, IAccessCheckService accessCheckService)
        {
            this.infoListRepository = infoListRepository;
            this.accessCheckService = accessCheckService;
        }

        public async Task Handle(DeleteInfoListCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToInfoList(request.UserId, request.Id))
            {
                throw new InfoListNotFoundException(request.Id);
            }
            
            await infoListRepository.DeleteByIdAsync(request.Id);
        }
    }
}