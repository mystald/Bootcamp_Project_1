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
        public IQueryable<DtoUserGet> GetAllUser([Service] IUser _user)
        {
            var results = _user.GetAll();
            return _mapper.ProjectTo<DtoUserGet>(results);
        }

        public async Task<DtoUserGet> GetUserById([Service] IUser _user, int id)
        {
            var result = await _user.GetById(id);
            return _mapper.Map<DtoUserGet>(result);
        }

        public async Task<IQueryable<DtoRoleGet>> GetRoleByUserId([Service] IUser _user, int id)
        {
            var result = await _user.GetRoles(id);
            return _mapper.ProjectTo<DtoRoleGet>(result);
        }

        public IQueryable<DtoTwittorGet> GetTwittorsByUserId([Service] IUser _user, int id)
        {
            var result = _user.GetTwittors(id);
            return result;
        }
    }
}