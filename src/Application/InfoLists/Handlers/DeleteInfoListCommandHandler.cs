using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Core.InfoLists.Repositories.Base;
using Application.Common;

namespace Application.InfoLists.Handlers
{
    public class DeleteInfoListCommandHandler : IRequestHandler<DeleteInfoListCommand, Result<bool>>
    {
        private readonly IInfoListRepository infoListRepository;

        public DeleteInfoListCommandHandler(IInfoListRepository infoListRepository)
        {
            this.infoListRepository = infoListRepository;
        }

        public async Task<Result<bool>> Handle(DeleteInfoListCommand request, CancellationToken cancellationToken)
        {
            await infoListRepository.DeleteByIdAsync(request.Id);
            return Result<bool>.Success(true);
        }
    }
}