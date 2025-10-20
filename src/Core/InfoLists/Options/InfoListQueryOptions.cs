using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.Options;

namespace Core.InfoLists.Options
{
    public class InfoListQueryOptions
    {
        public bool Id { get; set; } = true;
        public bool Name { get; set; } = false;
        public DynamicItemQueryOptions? Items { get; set; } = null;
    }
}