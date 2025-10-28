using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.InfoTexts.Models
{
    public class InfoText
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Text { get; set; }
        public Guid BusinessId { get; set; }
    }
}