using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Core.InfoLists.Repositories.Base;
using AutoMapper;
using Application.InfoLists.Exceptions;
using Application.Users.Services.Base;
using System;

namespace Application.InfoLists.Handlers
{
    public class UpdateInfoListNameCommandHandler : IRequestHandler<UpdateInfoListNameCommand, string>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IAccessCheckService accessCheckService;

        public UpdateInfoListNameCommandHandler(IInfoListRepository infoListRepository, IAccessCheckService accessCheckService)
        {
            this.infoListRepository = infoListRepository;
            this.accessCheckService = accessCheckService;
        }

        public async Task<string> Handle(UpdateInfoListNameCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToInfoList(request.InfoList.UserId, request.InfoList.Id))
            {
                throw new InfoListNotFoundException(request.InfoList.Id);
            }

            var newName = await infoListRepository.SetNameByIdAsync(request.InfoList.Id, request.InfoList.Name);
            if (newName == null)
            {
                throw new InfoListNotFoundException(request.InfoList.Id);
            }
            return newName;
        }
    }
}