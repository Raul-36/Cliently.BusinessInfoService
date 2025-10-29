using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Core.InfoTexts.Repositories.Base;

namespace Application.InfoTexts.Handlers
{
    public class DeleteInfoTextCommandHandler : IRequestHandler<DeleteInfoTextCommand, bool>
    {
        private readonly IInfoTextRepository infoTextRepository;

        public DeleteInfoTextCommandHandler(IInfoTextRepository infoTextRepository)
        {
            this.infoTextRepository = infoTextRepository;
        }

        public async Task<bool> Handle(DeleteInfoTextCommand request, CancellationToken cancellationToken)
        {
            await infoTextRepository.DeleteByIdAsync(request.Id);
            return true;
        }
    }
}