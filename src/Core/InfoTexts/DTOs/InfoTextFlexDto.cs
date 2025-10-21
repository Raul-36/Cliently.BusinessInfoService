using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.InfoTexts.DTOs
{
    public class InfoTextFlexDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }
    }
}