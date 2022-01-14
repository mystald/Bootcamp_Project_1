using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraphQLAPI.Dtos;
using GraphQLAPI.Models;

namespace GraphQLAPI.Profiles
{
    public class ProfileRole : Profile
    {
        public ProfileRole()
        {
            CreateMap<Role, DtoRoleGet>();
        }
    }
}