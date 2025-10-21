using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DynamicItems.Models;

public class DynamicItem
{
    public required Guid Id { get; set; }
    public required Dictionary<string, object> Properties = new();
}