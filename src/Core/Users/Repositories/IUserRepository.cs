using System;
using System.Threading.Tasks;
using Core.Users.Entities;

namespace Core.Users.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task DeleteAsync(Guid userId);
        Task<User?> GetByIdAsync(Guid userId);
    }
}
