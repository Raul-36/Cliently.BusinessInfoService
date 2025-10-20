using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InfoTexts.Models;
using Core.InfoTexts.Options;

namespace Core.InfoTexts.Repositories.Base
{
    public interface IInfoTextRepository
    {
        public Task<IEnumerable<InfoText>> GetAllByBusinessIdAsync(Guid businessId, InfoTextQueryOptions options);
        public Task<InfoText> GetByIdAsync(Guid id);
        public Task<InfoText> GetByIdAsync(Guid id, InfoTextQueryOptions options);
        public Task<InfoText> AddAsync(InfoText infoText, Guid businessId);
        public Task<InfoText> UpdateAsync(InfoText infoText);
        public Task DeleteByIdAsync(Guid id);
    }
}