using System;
using System.Threading.Tasks;
using Core.Users.Entities;
using Core.Users.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users.Repositories
{
    public class UserEFPostgreRepository : IUserRepository
    {
        private readonly BusinessInfoEFPostgreContext context;

        public UserEFPostgreRepository(BusinessInfoEFPostgreContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
