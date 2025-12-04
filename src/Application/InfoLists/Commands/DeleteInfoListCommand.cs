using MediatR;
using System;

namespace Application.InfoLists.Commands
{
    public class DeleteInfoListCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}