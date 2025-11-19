using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Core.Businesses.Repositories.Base;
using AutoMapper;
using Application.Businesses.Exceptions;

namespace Application.Businesses.Handlers
{
    public class UpdateBusinessNameCommandHandler : IRequestHandler<UpdateBusinessNameCommand, string>
    {
        private readonly IBusinessRepository businessRepository;

        public UpdateBusinessNameCommandHandler(IBusinessRepository businessRepository)
        {
            this.businessRepository = businessRepository;
        }

        public async Task<string> Handle(UpdateBusinessNameCommand request, CancellationToken cancellationToken)
        {
            var newName = await businessRepository.SetNameByIdAsync(request.Business.Id, request.Business.Name);
            if (newName == null)
                throw new BusinessNotFoundException(request.Business.Id);
            
            return newName;
        }
    }
}