using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.Models;
using Core.DynamicItems.Options;

namespace Core.DynamicItems.Repositories.Base
{
    public interface IDynamicItemRepository
    {
        public Task<IEnumerable<DynamicItem>> GetAllByListIdAsync(Guid listId, DynamicItemQueryOptions options);
        public Task<DynamicItem> GetByIdAsync(Guid id);
        public Task<DynamicItem> GetByIdAsync(Guid id, DynamicItemQueryOptions options);
        public Task<DynamicItem> AddAsync(DynamicItem DynamicItem, Guid listId);
        public Task<DynamicItem> UpdateAsync(DynamicItem DynamicItem);
        public Task DeleteByIdAsync(Guid id);
    }
}