using MediatR;
using Application.InfoTexts.DTOs.Requests;
using Application.InfoTexts.DTOs.Responses;
using Application.Common;

namespace Application.InfoTexts.Commands
{
    public class UpdateInfoTextCommand : IRequest<Result<InfoTextResponse>>
    {
        public UpdateInfoTextRequest InfoText { get; set; }
    }
}