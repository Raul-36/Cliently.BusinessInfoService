using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Core.InfoTexts.Repositories.Base;

namespace Application.InfoTexts.Handlers
{
    public class DeleteInfoTextCommandHandler : IRequestHandler<DeleteInfoTextCommand>
    {
        private readonly IInfoTextRepository infoTextRepository;

        public DeleteInfoTextCommandHandler(IInfoTextRepository infoTextRepository)
        {
            this.infoTextRepository = infoTextRepository;
        }

        public async Task Handle(DeleteInfoTextCommand request, CancellationToken cancellationToken)
        {
            await infoTextRepository.DeleteByIdAsync(request.Id);
            return;
        }
    }
}