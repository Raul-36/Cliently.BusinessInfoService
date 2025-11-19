using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InfoTexts.Entities;

namespace Core.InfoTexts.Repositories.Base
{
    public interface IInfoTextRepository
    {
        public Task<IEnumerable<InfoText>> GetAllByBusinessIdAsync(Guid businessId);
        public Task<InfoText?> GetByIdAsync(Guid id);
        public Task<InfoText> AddAsync(InfoText infoText);
        public Task<InfoText?> UpdateAsync(InfoText infoText);
        public Task DeleteByIdAsync(Guid id);
    }
}