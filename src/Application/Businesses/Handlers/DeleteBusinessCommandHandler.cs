using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Application.Users.Services.Base;
using Core.Businesses.Repositories.Base;
using Application.Businesses.Exceptions;

namespace Application.Businesses.Handlers
{
    public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IAccessCheckService accessCheckService;

        public DeleteBusinessCommandHandler(IBusinessRepository businessRepository, IAccessCheckService accessCheckService)
        {
            this.businessRepository = businessRepository;
            this.accessCheckService = accessCheckService;
        }

        public async Task Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToBusiness(request.UserId, request.Id))
            {
                throw new BusinessNotFoundException(request.Id);
            }
            
            await businessRepository.DeleteByIdAsync(request.Id);
        }
    }
}