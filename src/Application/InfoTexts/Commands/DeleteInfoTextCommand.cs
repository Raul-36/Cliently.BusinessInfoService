using MediatR;
using System;

namespace Application.InfoTexts.Commands
{
    public class DeleteInfoTextCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}