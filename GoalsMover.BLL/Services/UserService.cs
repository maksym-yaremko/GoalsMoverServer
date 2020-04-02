using AutoMapper;
using GoalsMover.BLL.IServices;
using GoalsMover.DAL;
using GoalsMover.DAL.Entities;
using GoalsMover.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Create(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            await _unitOfWork.UserRepository.Create(user);
            _unitOfWork.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public async Task<UserDTO> Get(int id)
        {
            var user = await _unitOfWork.UserRepository.Get(id);
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            var usersDTO = new List<UserDTO>();

            foreach (var user in users)
            {
                usersDTO.Add(_mapper.Map<UserDTO>(user));
            }

            return usersDTO;
        }

        public async Task Update(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            await _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }
    }
}
