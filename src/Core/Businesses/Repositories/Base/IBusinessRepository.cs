using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Businesses.Entities;

namespace Core.Businesses.Repositories.Base
{
    public interface IBusinessRepository
    {
        public Task<Business?> GetByIdAsync(Guid id);
        public Task<IEnumerable<Business>> GetAllAsync();
        public Task<Business> AddAsync(Business business);
        public Task<string?> SetNameByIdAsync(Guid id, string newName);
        public Task DeleteByIdAsync(Guid id);
    }
}