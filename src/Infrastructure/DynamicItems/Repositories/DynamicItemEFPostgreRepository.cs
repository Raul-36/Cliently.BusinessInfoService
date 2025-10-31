using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DynamicItems.Models;
using Core.DynamicItems.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DynamicItems.Repositories
{
    public class DynamicItemEFPostgreRepository : IDynamicItemRepository
    {
        private readonly BusinessInfoEFPostgreContext context;

        public DynamicItemEFPostgreRepository(BusinessInfoEFPostgreContext context)
        {
            this.context = context;
        }

        public async Task<DynamicItem> AddAsync(DynamicItem dynamicItem)
        {
            var added = (await context.DynamicItems.AddAsync(dynamicItem)).Entity;
            await context.SaveChangesAsync();
            return added;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await context.DynamicItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return;

            context.DynamicItems.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DynamicItem>> GetAllByListIdAsync(Guid listId)
        {
            return await context.DynamicItems
                .AsNoTracking()
                .Where(x => x.ListId == listId)
                .Select(x => new DynamicItem()
                {
                    Id = x.Id,
                    ListId = x.ListId,
                    Name = x.Name,
                    Properties = null!
                })
                .ToListAsync();
        }

        public async Task<DynamicItem?> GetByIdAsync(Guid id)
        {
            var entity = await context.DynamicItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return entity;
        }

        public async Task<DynamicItem?> UpdateAsync(DynamicItem dynamicItem)
        {
            var existing = await context.DynamicItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == dynamicItem.Id);

            if (existing == null)
                return null;

            dynamicItem.ListId = existing.ListId;
            var updated = context.DynamicItems.Update(dynamicItem).Entity;
            await context.SaveChangesAsync();

            return updated;
        }
    }
}
