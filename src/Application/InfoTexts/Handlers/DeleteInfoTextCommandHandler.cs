using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Core.InfoTexts.Repositories.Base;
using Application.Common;

namespace Application.InfoTexts.Handlers
{
    public class DeleteInfoTextCommandHandler : IRequestHandler<DeleteInfoTextCommand, Result<bool>>
    {
        private readonly IInfoTextRepository infoTextRepository;

        public DeleteInfoTextCommandHandler(IInfoTextRepository infoTextRepository)
        {
            this.infoTextRepository = infoTextRepository;
        }

        public async Task<Result<bool>> Handle(DeleteInfoTextCommand request, CancellationToken cancellationToken)
        {
            await infoTextRepository.DeleteByIdAsync(request.Id);
            return Result<bool>.Success(true);
        }
    }
}