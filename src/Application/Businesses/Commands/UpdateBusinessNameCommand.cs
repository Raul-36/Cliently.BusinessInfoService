using MediatR;
using Application.Businesses.DTOs.Requests;

namespace Application.Businesses.Commands
{
    public class UpdateBusinessNameCommand : IRequest<string>
    {
        public required UpdateBusinessRequest Business { get; set; }
    }
}