using GoalsMover.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.DAL.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAndPassword(string email,string password);
        Task<User> GetByEmail(string email);
    }
}
