using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InfoLists.Models;

namespace Core.InfoLists.Repositories.Base
{
    public interface IInfoListRepository
    {
        public Task<IEnumerable<InfoList>> GetAllByBusinessIdAsync(Guid businessId);
        public Task<InfoList> GetByIdAsync(Guid id);
        public Task<string> SetNameByIdAsync(Guid id, string name); 
        public Task<InfoList> AddAsync(InfoList InfoList);
        public Task DeleteByIdAsync(Guid id);
    }
}