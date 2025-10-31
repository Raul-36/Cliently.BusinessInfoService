using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InfoTexts.Models;
using Core.InfoTexts.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.InfoTexts.Repositories
{
    public class InfoTextEFPostgreRepository : IInfoTextRepository
    {
        private readonly BusinessInfoEFPostgreContext context;

        public InfoTextEFPostgreRepository(BusinessInfoEFPostgreContext context)
        {
            this.context = context;
        }

        public async Task<InfoText> AddAsync(InfoText infoText)
        {
            var added = (await context.InfoTexts.AddAsync(infoText)).Entity;
            await context.SaveChangesAsync();
            return added;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await context.InfoTexts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return;

            context.InfoTexts.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InfoText>> GetAllByBusinessIdAsync(Guid businessId)
        {
            return await context.InfoTexts
                .AsNoTracking()
                .Where(x => x.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task<InfoText?> GetByIdAsync(Guid id)
        {
            var entity = await context.InfoTexts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return entity;
        }

        public async Task<InfoText?> UpdateAsync(InfoText infoText)
        {
            var existing = await context.InfoTexts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == infoText.Id);
            if (existing == null)
                return null;

            infoText.BusinessId = existing.BusinessId;

            var updated = context.InfoTexts.Update(infoText).Entity;
            await context.SaveChangesAsync();

            return updated;
        }
    }
}