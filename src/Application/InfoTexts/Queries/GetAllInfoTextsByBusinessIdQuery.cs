using MediatR;
using System;
using System.Collections.Generic;
using Application.InfoTexts.DTOs.Responses;
using Application.Common;

namespace Application.InfoTexts.Queries
{
    public class GetAllInfoTextsByBusinessIdQuery : IRequest<Result<IEnumerable<InfoTextResponse>>>
    {
        public Guid BusinessId { get; set; }
    }
}