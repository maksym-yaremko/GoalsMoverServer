using GoalsMover.DAL.Context;
using GoalsMover.DAL.Entities;
using GoalsMover.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GoalsMoverDbContext _dbContext;

        public UserRepository(GoalsMoverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(User user)
        {
            _dbContext.Users.Add(user);
        }

        public async Task<User> Get(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            _dbContext.Users.Remove(user);
        }

        public async Task Update(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
        }

        public async Task<User> GetByEmailAndPassword(string email,string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
