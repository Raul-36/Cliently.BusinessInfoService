using MediatR;
using System;
using Application.InfoTexts.DTOs.Responses;
using Application.Common;

namespace Application.InfoTexts.Queries
{
    public class GetInfoTextByIdQuery : IRequest<Result<InfoTextResponse>>
    {
        public Guid Id { get; set; }
    }
}