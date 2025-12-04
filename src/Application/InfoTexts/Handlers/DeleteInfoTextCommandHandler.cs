using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.Users.Services.Base;
using Core.InfoTexts.Repositories.Base;
using Application.InfoLists.Exceptions;
using Application.InfoTexts.Exceptions;

namespace Application.InfoTexts.Handlers
{
    public class DeleteInfoTextCommandHandler : IRequestHandler<DeleteInfoTextCommand>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IAccessCheckService accessCheckService;

        public DeleteInfoTextCommandHandler(IInfoTextRepository infoTextRepository, IAccessCheckService accessCheckService)
        {
            this.infoTextRepository = infoTextRepository;
            this.accessCheckService = accessCheckService;
        }

        public async Task Handle(DeleteInfoTextCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToInfoText(request.UserId, request.Id))
            {
                throw new InfoTextNotFoundException(request.Id);
            }
            
            await infoTextRepository.DeleteByIdAsync(request.Id);
        }
    }
}