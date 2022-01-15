using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraphQLAPI.Dtos;
using GraphQLAPI.Models;

namespace GraphQLAPI.Profiles
{
    public class ProfileComment : Profile
    {
        public ProfileComment()
        {
            CreateMap<Comment, DtoCommentGet>();
            CreateMap<DtoCommentInput, Comment>()
                .ForMember(c => c.PostDate, opt => opt.MapFrom(input => DateTime.Now));
        }
    }
}