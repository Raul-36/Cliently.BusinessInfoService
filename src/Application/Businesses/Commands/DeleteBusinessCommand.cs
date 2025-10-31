using MediatR;
using Application.Common;

namespace Application.Businesses.Commands
{
    public class DeleteBusinessCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}