using GoalsMover.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.DAL
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        void SaveChanges();
    }
}
