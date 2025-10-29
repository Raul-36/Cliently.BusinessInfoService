using MediatR;
using System;
using Application.InfoTexts.DTOs.Responses;

namespace Application.InfoTexts.Queries
{
    public class GetInfoTextByIdQuery : IRequest<InfoTextResponse>
    {
        public Guid Id { get; set; }
    }
}