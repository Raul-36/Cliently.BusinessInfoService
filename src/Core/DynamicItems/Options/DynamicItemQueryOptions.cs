using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DynamicItems.Options
{
    public class DynamicItemQueryOptions
    {
        public bool Id { get; set; }
        public required Dictionary<string, bool> Properties { get; set; } = new Dictionary<string, bool>();

    }
}
