using AutoMapper;
using GoalsMover.BLL.Helpers;
using GoalsMover.BLL.Helpers.Methods;
using GoalsMover.BLL.IServices;
using GoalsMover.DAL;
using GoalsMover.DAL.Entities;
using GoalsMover.DTO.DTO;
using GoalsMover.DTO.Model;
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

        public async Task<UserDTO> Authenticate(LoginModel loginModel)
        {

            var user = await _unitOfWork.UserRepository.GetByEmailAndPassword(loginModel.Email, loginModel.Password);

            if (user == null)
            {
                return null;
            }
            user.Token = AccessToken.GenerateToken(user,_sensitiveTokens);
            user.RefreshToken = RefreshToken.GenerateToken();
            var expirationDate = DateTime.Now.AddDays(_sensitiveTokens.RefreshTokenLifetime).ToString("yyyy-MM-dd HH:mm:ss");
            user.RefreshTokenExiparionDate = DateTime.ParseExact(expirationDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            await _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();

            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task Create(SignupModel signupModel)
        {
            var user = new User
            {
                Email = signupModel.Email,
                Password = signupModel.Password
            };

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

        public async Task<UserDTO> RefreshAccessToken(RefreshTokensModel refreshTokensModel)
        {
            var principal = Principal.GetPrincipalFromExpiredToken(refreshTokensModel.AccessToken, _sensitiveTokens);
            var email = principal.Identity.Name;

            var user = await _unitOfWork.UserRepository.GetByEmail(email);

            if (user.RefreshToken != null)
            {
                var savedRefreshToken = user.RefreshToken;
                if (savedRefreshToken != refreshTokensModel.RefreshToken)
                {
                    throw new SecurityTokenException("Invalid refresh token");
                }

                else
                {
                    user.Token = AccessToken.GenerateToken(user, _sensitiveTokens);
                    var currentTime = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    if (currentTime > user.RefreshTokenExiparionDate)
                    {
                        user.RefreshToken = RefreshToken.GenerateToken();
                        user.RefreshTokenExiparionDate = DateTime.Now.Date.AddDays(_sensitiveTokens.RefreshTokenLifetime);
                    }

                    await _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.SaveChanges();

                    var userDTO = _mapper.Map<UserDTO>(user);

                    return userDTO;
                }
            }
            else
            {
                throw new SecurityTokenException("No refresh token id db");
            }

        }

        public Task Update(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        //public async Task Update(EditUserModel editUserModel)
        //{
        //    var user = new User
        //    {

        //    };
        //    await _unitOfWork.UserRepository.Update(user);
        //    _unitOfWork.SaveChanges();
        //}
    }
}
