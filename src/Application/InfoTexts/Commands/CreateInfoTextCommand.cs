using MediatR;
using Application.InfoTexts.DTOs.Requests;
using Application.InfoTexts.DTOs.Responses;
using Application.Common;

namespace Application.InfoTexts.Commands
{
    public class CreateInfoTextCommand : IRequest<Result<InfoTextResponse>>
    {
        public required CreateInfoTextRequest InfoText { get; set; }
    }
}