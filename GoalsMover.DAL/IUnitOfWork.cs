using GoalsMover.DAL.Entities;
using GoalsMover.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.DAL
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        void SaveChanges();
    }
}
