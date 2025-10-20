using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.InfoTexts.Options
{
    public class InfoTextQueryOptions
    {
        public bool Id { get; set; } = true;
        public bool Name { get; set; } = false; 
        public bool Text { get; set; } = false;
    }
}