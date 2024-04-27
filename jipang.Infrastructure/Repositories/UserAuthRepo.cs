using jipang.Domain.Entities;
using jipang.Infrastructure.Data;
using jipang.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jipang.Infrastructure.Repositories
{
    public class UserAuthRepo : IUserAuthRepo
    {
        private readonly AppDbContext _dbContext;

        public UserAuthRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Users?> GetByUsername(string username)
        {
            return await _dbContext.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }
    }
}
