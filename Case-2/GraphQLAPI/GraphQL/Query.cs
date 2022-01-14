using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GraphQLAPI.Data;
using GraphQLAPI.Dtos;
using GraphQLAPI.Models;
using HotChocolate;

namespace GraphQLAPI.GraphQL
{
    public class Query
    {
        private IMapper _mapper;

        public Query(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<IQueryable<DtoUserGet>> GetAllUser([Service] IUser _user)
        {
            var results = await _user.GetAll();
            return _mapper.ProjectTo<DtoUserGet>(results);
        }
    }
}