using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraphQLAPI.Data;
using GraphQLAPI.Dtos;
using GraphQLAPI.Exceptions;
using GraphQLAPI.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;

namespace GraphQLAPI.GraphQL
{
    public class Mutation
    {
        private IMapper _mapper;

        public Mutation(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<DtoReturnDataSuccess<string>> Authentication([Service] IUser _user, DtoUserAuth input)
        {
            return new DtoReturnDataSuccess<string>
            {
                data = await _user.Authentication(input.Username, input.Password)
            };
        }

        public async Task<DtoReturnDataSuccess<DtoUserGet>> Registration([Service] IUser _user, DtoUserInput input)
        {
            var newUser = await _user.Insert(_mapper.Map<User>(input));
            // TODO Assign role "MEMBER" to newUser
            return new DtoReturnDataSuccess<DtoUserGet>
            {
                data = _mapper.Map<DtoUserGet>(newUser)
            };
        }

        [Authorize(Roles = new string[] { "ADMIN", "MEMBER" })]
        public async Task<DtoReturnDataSuccess<DtoUserGet>> EditProfile([Service] IUser _user, int id, DtoUserEditProfile input)
        {
            // TODO Get UserId from JWT Claim
            var result = await _user.Update(id, _mapper.Map<User>(input));
            return new DtoReturnDataSuccess<DtoUserGet>
            {
                data = _mapper.Map<DtoUserGet>(result)
            };
        }

        [Authorize(Roles = new string[] { "ADMIN", "MEMBER" })]
        public async Task<DtoReturnDataSuccess> ChangePassword([Service] IUser _user, int id, DtoUserChangePass input)
        {
            // TODO Get UserId from JWT Claim
            var username = (await _user.GetById(id)).Username;

            try
            {
                await _user.ValidatePass(username, input.oldPassword);
            }
            catch (InvalidCredentialsException)
            {
                throw new InvalidCredentialsException("Invalid Old Password");
            }

            var result = await _user.Update(
                id,
                new User { Password = input.newPassword }
            );

            return new DtoReturnDataSuccess();
        }

        [Authorize(Roles = new string[] { "ADMIN" })]
        public async Task<DtoReturnDataSuccess<DtoUserGet>> LockUser([Service] IUser _user, int id, bool isLock)
        {
            var result = await _user.Update(
                id,
                new User { isLocked = isLock }
            );

            return new DtoReturnDataSuccess<DtoUserGet>
            {
                data = _mapper.Map<DtoUserGet>(result)
            };
        }

        [Authorize(Roles = new string[] { "ADMIN" })]
        public async Task<DtoReturnDataSuccess<DtoUserGet>> DeleteUser([Service] IUser _user, int id)
        {
            var result = await _user.Delete(id);

            return new DtoReturnDataSuccess<DtoUserGet>
            {
                data = _mapper.Map<DtoUserGet>(result)
            };
        }

        [Authorize(Roles = new string[] { "ADMIN" })]

        public async Task<DtoReturnDataSuccess<UserRole>> AssignUserRole([Service] IUserRole _userRole, int userId, int roleId)
        {
            var result = await _userRole.Insert(
                new UserRole
                {
                    UserId = userId,
                    RoleId = roleId,
                }
            );

            return new DtoReturnDataSuccess<UserRole>
            {
                data = result
            };
        }

        [Authorize(Roles = new string[] { "ADMIN" })]

        public async Task<DtoReturnDataSuccess<UserRole>> UnassignUserRole([Service] IUserRole _userRole, int userId, int roleId)
        {
            var result = await _userRole.Delete(userId, roleId);

            return new DtoReturnDataSuccess<UserRole>
            {
                data = result
            };
        }

        [Authorize(Roles = new string[] { "MEMBER" })]
        public async Task<DtoReturnDataSuccess<DtoTwittorGet>> PostTwittor([Service] ITwittor _twittor, DtoTwitterInput input)
        {
            var result = await _twittor.Insert(_mapper.Map<Twittor>(input));

            return new DtoReturnDataSuccess<DtoTwittorGet>
            {
                data = _mapper.Map<DtoTwittorGet>(result)
            };
        }

        [Authorize(Roles = new string[] { "MEMBER" })]
        public async Task<DtoReturnDataSuccess<DtoTwittorGet>> UpdateTwittor([Service] ITwittor _twittor, int id, string content)
        {
            var result = await _twittor.Update(
                id,
                new Twittor { Content = content }
            );

            return new DtoReturnDataSuccess<DtoTwittorGet>
            {
                data = _mapper.Map<DtoTwittorGet>(result)
            };
        }

        [Authorize(Roles = new string[] { "MEMBER" })]
        public async Task<DtoReturnDataSuccess<DtoTwittorGet>> DeleteTwittor([Service] ITwittor _twittor, int id)
        {
            var result = await _twittor.Delete(id);

            return new DtoReturnDataSuccess<DtoTwittorGet>
            {
                data = _mapper.Map<DtoTwittorGet>(result)
            };
        }

        [Authorize(Roles = new string[] { "MEMBER" })]
        public async Task<DtoReturnDataSuccess<DtoCommentGet>> PostComment([Service] IComment _comment, DtoCommentInput input)
        {
            var result = await _comment.Insert(_mapper.Map<Comment>(input));

            return new DtoReturnDataSuccess<DtoCommentGet>
            {
                data = _mapper.Map<DtoCommentGet>(result)
            };
        }

        [Authorize(Roles = new string[] { "MEMBER" })]
        public async Task<DtoReturnDataSuccess<DtoCommentGet>> UpdateComment([Service] IComment _comment, int id, string content)
        {
            var result = await _comment.Update(
                id,
                new Comment { Content = content }
            );

            return new DtoReturnDataSuccess<DtoCommentGet>
            {
                data = _mapper.Map<DtoCommentGet>(result)
            };
        }

        [Authorize(Roles = new string[] { "MEMBER" })]
        public async Task<DtoReturnDataSuccess<DtoCommentGet>> DeleteComment([Service] IComment _comment, int id)
        {
            var result = await _comment.Delete(id);

            return new DtoReturnDataSuccess<DtoCommentGet>
            {
                data = _mapper.Map<DtoCommentGet>(result)
            };
        }
    }
}
