using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DynamicItems.Entities;

public class DynamicItem
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Dictionary<string, object>? Properties;
    public Guid ListId { get; set; }
}