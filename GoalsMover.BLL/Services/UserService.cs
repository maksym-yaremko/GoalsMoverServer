using AutoMapper;
using GoalsMover.BLL.Helpers;
using GoalsMover.BLL.Helpers.Methods;
using GoalsMover.BLL.IServices;
using GoalsMover.DAL;
using GoalsMover.DAL.Entities;
using GoalsMover.DTO.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GoalsMover.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SensitiveTokens _sensitiveTokens;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<SensitiveTokens> sensitiveTokens)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sensitiveTokens = sensitiveTokens.Value;
        }

        public async Task<User> Authenticate(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(email);

            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_sensitiveTokens.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
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
