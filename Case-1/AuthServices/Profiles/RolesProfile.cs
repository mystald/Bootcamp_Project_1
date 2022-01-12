using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Dtos;
using AuthServices.Models;
using AutoMapper;

namespace AuthServices.Profiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<Role, DtoRoleGet>();
        }
    }
}