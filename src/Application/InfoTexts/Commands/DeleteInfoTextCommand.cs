using MediatR;
using System;
using Application.Common;

namespace Application.InfoTexts.Commands
{
    public class DeleteInfoTextCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}