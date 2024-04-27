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
    public class UserRepo : IUserRepo
    {
        public readonly AppDbContext _dbContext;
        public UserRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Users> GetById(int id)
        {
            return await _dbContext.Users
                .FindAsync(id);
        }

        public async Task Add(Users users)
        { 
            await _dbContext.Users.AddAsync(users);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Users users) 
        {
            _dbContext.Entry(users).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Users users)
        { 
            _dbContext.Users.Remove(users);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _dbContext.Users
                .ToListAsync();
        }
    }
}
