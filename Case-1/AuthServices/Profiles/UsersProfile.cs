using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Dtos;
using AuthServices.Helpers;
using AuthServices.Models;
using AutoMapper;

namespace AuthServices.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, DtoUserGet>();
            CreateMap<DtoUserAdminRegisterInput, User>()
                .ForMember(usr => usr.Password,
                opt => opt.MapFrom(input => Hash.getHash(input.Password)));
        }
    }
}