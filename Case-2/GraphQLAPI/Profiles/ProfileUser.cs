using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraphQLAPI.Dtos;
using GraphQLAPI.Helpers;
using GraphQLAPI.Models;

namespace GraphQLAPI.Profiles
{
    public class ProfileUser : Profile
    {
        public ProfileUser()
        {
            CreateMap<User, DtoUserGet>();
            CreateMap<DtoUserInput, User>()
                .ForMember(usr => usr.Password,
                opt => opt.MapFrom(input => Hash.getHash(input.Password))); ;
            CreateMap<DtoUserEditProfile, User>();
        }
    }
}