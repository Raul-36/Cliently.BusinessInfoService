using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.Models;

namespace Core.DynamicItems.Repositories.Base
{
    public interface IDynamicItemRepository
    {
        public Task<IEnumerable<DynamicItem>> GetAllByListIdAsync(Guid listId);
        public Task<DynamicItem> GetByIdAsync(Guid id);
        public Task<DynamicItem> AddAsync(DynamicItem dynamicItem);
        public Task<DynamicItem> UpdateAsync(DynamicItem dynamicItem);
        public Task DeleteByIdAsync(Guid id);
    }
}