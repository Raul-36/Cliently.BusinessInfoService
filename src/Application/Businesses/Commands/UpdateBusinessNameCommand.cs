using MediatR;
using Application.Businesses.DTOs.Requests;
using Application.Common;

namespace Application.Businesses.Commands
{
    public class UpdateBusinessNameCommand : IRequest<Result<string>>
    {
        public required UpdateBusinessRequest Business { get; set; }
    }
}