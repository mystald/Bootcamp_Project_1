using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GraphQLAPI.Data;
using GraphQLAPI.Dtos;
using GraphQLAPI.Kafka;
using GraphQLAPI.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;

namespace GraphQLAPI.GraphQL
{
    [Authorize]
    public class Query
    {
        private IMapper _mapper;
        private Producer _kafka;

        public Query(IMapper mapper, Producer kafka)
        {
            _mapper = mapper;
            _kafka = kafka;
        }

        public IQueryable<DtoUserGet> GetAllUser([Service] IUser _user)
        {
            var results = _user.GetAll();

            _kafka.SendMessage("loggings", "user-get", "GetAllUser");

            return _mapper.ProjectTo<DtoUserGet>(results);
        }

        public async Task<DtoUserGet> GetUserById([Service] IUser _user, int id)
        {
            var result = await _user.GetById(id);

            _kafka.SendMessage("loggings", "user-get", "GetUserById");

            return _mapper.Map<DtoUserGet>(result);
        }

        [Authorize(Roles = new string[] { "ADMIN" })]
        public async Task<IQueryable<DtoRoleGet>> GetRoleByUserId([Service] IUser _user, int id)
        {
            var result = await _user.GetRoles(id);

            _kafka.SendMessage("loggings", "role-get", "GetRoleByUserId");

            return _mapper.ProjectTo<DtoRoleGet>(result);
        }

        public IQueryable<DtoTwittorGet> GetTwittorsByUserId([Service] IUser _user, int id)
        {
            var result = _user.GetTwittors(id);

            _kafka.SendMessage("loggings", "twittor-get", "GetTwittorsByUserId");

            return result;
        }

        public IQueryable<DtoTwittorGet> GetAllTwittors([Service] ITwittor _twittor)
        {
            var result = _twittor.GetAll();

            _kafka.SendMessage("loggings", "twittor-get", "GetAllTwittors");

            return _mapper.ProjectTo<DtoTwittorGet>(result);
        }

        public async Task<IQueryable<DtoCommentGet>> GetCommentByTwittorId([Service] ITwittor _twittor, int id)
        {
            var result = await _twittor.GetCommentsByTwittorId(id);

            _kafka.SendMessage("loggings", "comment-get", "GetCommentByTwittorId");

            return _mapper.ProjectTo<DtoCommentGet>(result);
        }

        public IQueryable<DtoCommentGet> GetAllComments([Service] IComment _comment)
        {
            _kafka.SendMessage("loggings", "comment-get", "GetAllComments");

            return _mapper.ProjectTo<DtoCommentGet>(
                _comment.GetAll()
            );
        }

        public async Task<DtoCommentGet> GetCommentById([Service] IComment _comment, int id)
        {
            _kafka.SendMessage("loggings", "comment-get", "GetCommentById");

            return _mapper.Map<DtoCommentGet>(
                await _comment.GetById(id)
            );
        }
    }
}