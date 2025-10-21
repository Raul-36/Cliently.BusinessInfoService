using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.DTOs;
using Core.DynamicItems.Models;

namespace Core.InfoLists.DTOs
{
    public class InfoListFlexDto : IEnumerable<DynamicItemFlexDto>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<DynamicItemFlexDto>? Items { get; set; }

        public IEnumerator<DynamicItemFlexDto> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}