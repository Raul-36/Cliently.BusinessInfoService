using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.Models;

namespace Core.InfoLists.Models
{
    public class InfoList : IEnumerable<DynamicItem>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required IEnumerable<DynamicItem> Items { get; set; }

        public IEnumerator<DynamicItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}