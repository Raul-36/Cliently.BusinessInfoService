using MediatR;
using Application.InfoTexts.DTOs.Requests;
using Application.InfoTexts.DTOs.Responses;

namespace Application.InfoTexts.Commands
{
    public class CreateInfoTextCommand : IRequest<InfoTextResponse>
    {
        public required CreateInfoTextRequest InfoText { get; set; }
    }
}