using AutoMapper;
using GoalsMover.DAL.Entities;
using GoalsMover.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoalsMover.BLL.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
