using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InfoLists.Models;
using Core.InfoLists.Options;

namespace Core.InfoLists.Repositories.Base
{
    public interface IInfoListRepository
    {
        public Task<IEnumerable<InfoList>> GetAllByBusinessIdAsync(Guid businessId, InfoListQueryOptions options);
        public Task<InfoList> GetByIdAsync(Guid id);
        public Task<InfoList> GetByIdAsync(Guid id, InfoListQueryOptions options);
        public Task<InfoList> AddAsync(InfoList InfoList, Guid businessId);
        public Task DeleteByIdAsync(Guid id);
    }
}