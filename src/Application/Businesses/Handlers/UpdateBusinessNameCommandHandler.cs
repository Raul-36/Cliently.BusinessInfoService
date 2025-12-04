using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Businesses.Commands;
using Core.Businesses.Repositories.Base;
using Application.Businesses.Exceptions;
using Application.Users.Services.Base;
using System;
using System.Data.Common;

namespace Application.Businesses.Handlers
{
    public class UpdateBusinessNameCommandHandler : IRequestHandler<UpdateBusinessNameCommand, string>
    {
        private readonly IBusinessRepository businessRepository;
        private readonly IAccessCheckService accessCheckService;

        public UpdateBusinessNameCommandHandler(IBusinessRepository businessRepository, IAccessCheckService accessCheckService)
        {
            this.businessRepository = businessRepository;
            this.accessCheckService = accessCheckService;
        }

        public async Task<string> Handle(UpdateBusinessNameCommand request, CancellationToken cancellationToken)
        {
            var business = request.Business;

            
            if (!accessCheckService.ToBusiness(business.UserId, business.Id))
            {
                throw new BusinessNotFoundException(business.Id);
            }
            
            var newName = await businessRepository.SetNameByIdAsync(business.Id, business.Name);
            if (newName == null)
                throw new BusinessNotFoundException(business.Id);
            
            return newName;
        }
    }
}