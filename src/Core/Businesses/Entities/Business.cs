using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.Entities;
using Core.InfoLists.Entities;
using Core.InfoTexts.Entities;

namespace Core.Businesses.Entities
{
    public class Business
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public IEnumerable<InfoList> Lists { get; set; } = Enumerable.Empty<InfoList>();
        public IEnumerable<InfoText> Texts { get; set; } = Enumerable.Empty<InfoText>();
    }
}