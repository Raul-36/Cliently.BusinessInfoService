using MediatR;
using System;

namespace Application.InfoTexts.Commands
{
    public class DeleteInfoTextCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}