using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraphQLAPI.Dtos;
using GraphQLAPI.Models;

namespace GraphQLAPI.Profiles
{
    public class ProfileTwittor : Profile
    {
        public ProfileTwittor()
        {
            CreateMap<DtoTwittorInput, Twittor>()
                .ForMember(twit => twit.PostDate,
                opt => opt.MapFrom(input => DateTime.Now));

            CreateMap<Twittor, DtoTwittorGet>();
        }
    }
}