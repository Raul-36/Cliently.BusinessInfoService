using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Businesses.Options;

namespace Core.Businesses.Repositories.Base
{
    public interface IBusinessRepository
    {
        public Task<Business> GetByIdAsync(Guid id);
        public Task<Business> GetByIdAsync(Guid id, BusinessQueryOptions options);
        public Task<Business> AddAsync(Business business);
        public Task<Business> SetNameByIdAsync(Guid id, string newName);
        public Task DeleteByIdAsync(Guid id);
    }
}