using MediatR;
using System;

namespace Application.InfoLists.Commands
{
    public class DeleteInfoListCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}