using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Businesses.DTOs.Responses
{
    public class ShortBusinessResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}