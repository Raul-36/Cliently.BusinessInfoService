using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DynamicItems.Models
{
    public class DynamicItem : DynamicObject
    {
        public Guid Id { get; set; }
    }
}