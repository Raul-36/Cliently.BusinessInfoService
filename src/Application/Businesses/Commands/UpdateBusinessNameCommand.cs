using MediatR;
using System;

namespace Application.Businesses.Commands
{
    public class UpdateBusinessNameCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
    }
}