using GoalsMover.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalsMover.DAL.Context
{
    public class GoalsMoverDbContext :DbContext
    {
        public GoalsMoverDbContext(DbContextOptions<GoalsMoverDbContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
