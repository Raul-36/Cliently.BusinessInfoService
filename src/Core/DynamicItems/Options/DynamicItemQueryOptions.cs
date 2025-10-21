using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DynamicItems.Options
{
    public class DynamicItemQueryOptions
    {
        public Dictionary<string, bool> Fields { get; set; } = new Dictionary<string, bool>();
        public DynamicItemQueryOptions()
        {
            this.Fields.Add("Id", true);
        }
    }
}
