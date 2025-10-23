using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DynamicItems.DTOs
{
    public class DynamicItemFlexDto
    {
        public Guid? Id { get; set; }
        public Dictionary<string, object?>? Properties = new();
    }
}