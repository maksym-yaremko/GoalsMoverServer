using GoalsMover.DAL.Entities;
using GoalsMover.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.BLL.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> Get(int id);
        Task Create(UserDTO userDTO);
        Task Delete(int id);
        Task Update(UserDTO userDTO);
        Task<User> Authenticate(string email);
    }
}
