using MediatR;
using System;
using System.Collections.Generic;
using Application.InfoTexts.DTOs.Responses;

namespace Application.InfoTexts.Queries
{
    public class GetAllInfoTextsByBusinessIdQuery : IRequest<IEnumerable<InfoTextResponse>>
    {
        public Guid BusinessId { get; set; }
        public Guid UserId { get; set; }
    }
}