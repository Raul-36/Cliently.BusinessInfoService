using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InfoLists.Options;
using Core.InfoTexts.Options;

namespace Core.Businesses.Options
{
    public class BusinessQueryOptions
    {
        public bool Id { get; set; } = true;
        public bool Name { get; set; } = false;
        public InfoListQueryOptions? Lists { get; set; } = null;
        public InfoTextQueryOptions? Texts { get; set; } = null;
    }
}