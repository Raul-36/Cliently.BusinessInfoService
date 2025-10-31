using MediatR;
using System;
using Application.Common;

namespace Application.InfoLists.Commands
{
    public class DeleteInfoListCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}