using MediatR;
using System;

namespace Application.InfoLists.Commands
{
    public class DeleteInfoListCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}