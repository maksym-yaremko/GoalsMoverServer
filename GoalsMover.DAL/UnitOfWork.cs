using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalsMover.DAL.Context;
using GoalsMover.DAL.Entities;
using GoalsMover.DAL.IRepositories;
using GoalsMover.DAL.Repositories;

namespace GoalsMover.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoalsMoverDbContext _dbContext;
        public UnitOfWork(GoalsMoverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IUserRepository UserRepository => new UserRepository(_dbContext);

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
