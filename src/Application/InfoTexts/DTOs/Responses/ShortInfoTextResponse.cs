using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.InfoTexts.DTOs.Responses
{
    public class ShortInfoTextResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}