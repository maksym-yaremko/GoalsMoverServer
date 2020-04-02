using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.DAL
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        Task Delete(int id);
        Task Update(T item);
    }
}
