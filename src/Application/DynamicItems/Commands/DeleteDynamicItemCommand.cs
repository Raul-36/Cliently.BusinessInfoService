using MediatR;
using System;

namespace Application.DynamicItems.Commands
{
    public class DeleteDynamicItemCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}