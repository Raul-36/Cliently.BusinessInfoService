using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Businesses.Entities;
using Core.Businesses.Repositories.Base;
using Core.InfoLists.Entities;
using Core.InfoTexts.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Businesses.Repositories
{
    public class BusinessEFPostgreRepository : IBusinessRepository
    {
        private readonly BusinessInfoEFPostgreContext context;

        public BusinessEFPostgreRepository(BusinessInfoEFPostgreContext context)
        {
            this.context = context;
        }

        public async Task<Business> AddAsync(Business business)
        {
            var added = (await context.Businesses.AddAsync(business)).Entity;
            await context.SaveChangesAsync();
            return added;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await context.Businesses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return;

            context.Businesses.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Business>> GetAllAsync()
        {
            return await this.context.Businesses
                .AsNoTracking()
                .Select(x => new Business()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Lists = null!,
                    Texts = null!

                })
                .ToListAsync();
        }

        public async Task<Business?> GetByIdAsync(Guid id)
        {
            var entity = await context.Businesses
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new Business()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Lists = x.Lists.ToList(),
                    Texts = x.Texts.Select(t => new InfoText()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Text = string.Empty,
                        BusinessId = t.BusinessId
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            if (entity == null)
                return null!;

            return entity;
        }

        public async Task<string?> SetNameByIdAsync(Guid id, string newName)
        {
            var entity = await context.Businesses.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return null!;

            entity.Name = newName;
            await context.SaveChangesAsync();

            return entity.Name;
        }
    }
}
