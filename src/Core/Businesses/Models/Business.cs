using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.Models;
using Core.InfoLists.Models;
using Core.InfoTexts.Models;

namespace Core.Businesses.Models
{
    public class Business
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required IEnumerable<InfoList> Lists { get; set; } = Enumerable.Empty<InfoList>();
        public required IEnumerable<InfoText> Texts { get; set; } = Enumerable.Empty<InfoText>();
    }
}