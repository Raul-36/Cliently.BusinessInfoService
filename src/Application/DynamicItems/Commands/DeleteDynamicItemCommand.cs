using MediatR;
using System;

namespace Application.DynamicItems.Commands
{
    public class DeleteDynamicItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}