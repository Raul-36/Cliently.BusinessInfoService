using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DynamicItems.Options
{
    public class DynamicItemQueryOptions : Dictionary<string, bool>
    {
        public DynamicItemQueryOptions()
        {
            base.Add("Id", true);
        }
    }
}
