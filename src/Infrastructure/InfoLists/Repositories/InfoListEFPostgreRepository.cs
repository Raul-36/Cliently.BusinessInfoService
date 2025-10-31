using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Businesses.Models;
using Core.InfoLists.Models;
using Core.InfoLists.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.InfoLists.Repositories
{
    public class InfoListEFPostgreRepository : IInfoListRepository
    {
        private readonly BusinessInfoEFPostgreContext context;

        public InfoListEFPostgreRepository(BusinessInfoEFPostgreContext context)
        {
            this.context = context;
        }

        public async Task<InfoList> AddAsync(InfoList infoList)
        {
            var added = (await context.InfoLists.AddAsync(infoList)).Entity;
            await context.SaveChangesAsync();
            return added;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await context.InfoLists
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return;
            context.InfoLists.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InfoList>> GetAllByBusinessIdAsync(Guid businessId)
        {
            return await context.InfoLists
                .Where(x => x.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task<InfoList?> GetByIdAsync(Guid id)
        {
            var entity = await context.InfoLists
                .AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return entity;
        }

        public async Task<string?> SetNameByIdAsync(Guid id, string name)
        {
            var entity = await context.InfoLists.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"InfoList with id '{id}' not found.");

            entity.Name = name;
            await context.SaveChangesAsync();

            return entity.Name;
        }
    }
}
