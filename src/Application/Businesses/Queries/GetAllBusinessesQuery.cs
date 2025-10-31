using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Businesses.DTOs.Responses;
using MediatR;

namespace Application.Businesses.Queries
{
    public record GetAllBusinessesQuery : IRequest<IEnumerable<ShortBusinessResponse>>;
    
}