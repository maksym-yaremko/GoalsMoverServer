using GoalsMover.DAL.Entities;
using GoalsMover.DTO.DTO;
using GoalsMover.DTO.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.BLL.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> Get(int id);
        Task Create(SignupModel userDTO);
        Task Delete(int id);
        Task Update(UserDTO userDTO);
        Task<UserDTO> Authenticate(LoginModel userModel);
        Task<UserDTO> RefreshAccessToken(RefreshTokensModel refreshTokensModel);
    }
}
