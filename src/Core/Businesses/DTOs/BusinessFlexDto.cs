using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InfoLists.Models;
using Core.InfoTexts.Models;

namespace Core.Businesses.DTOs
{
    public class BusinessFlexDto
    {
        public Guid? Id { get; set; }
        public IEnumerable<InfoList>? Lists { get; set; }
        public IEnumerable<InfoText>? Texts { get; set; }
    }
}