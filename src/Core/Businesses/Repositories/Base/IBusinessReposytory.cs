using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Businesses.Options;

namespace Core.Businesses.Repositories.Base
{
    public interface IBusinessReposytory
    {
        public IEnumerable<Business> GetAll();
        public IEnumerable<Business> GetAll(BusinessQueryOptions options);
        public Business GetById(Guid Id);
        public Business GetById(Guid Id, BusinessQueryOptions options);
        public Business Add(Business business);
        public Business SetBussinasNameById(Guid Id, string newName);
        public void DeleteById(Guid Id);
    }
}