using MediatR;
using System;
using Application.Common;

namespace Application.DynamicItems.Commands
{
    public class DeleteDynamicItemCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}