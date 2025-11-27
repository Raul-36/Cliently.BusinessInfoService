using MediatR;
using Application.InfoTexts.DTOs.Requests;
using Application.InfoTexts.DTOs.Responses;

namespace Application.InfoTexts.Commands
{
    public class UpdateInfoTextCommand : IRequest<InfoTextResponse>
    {
        public required UpdateInfoTextRequest InfoText { get; set; }
    }
}