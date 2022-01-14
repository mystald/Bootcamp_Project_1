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

namespace GraphQLAPI.GraphQL
{
    public class Mutation
    {
        private IMapper _mapper;

        public Mutation(IMapper mapper)
        {
            _mapper = mapper;
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

        public async Task<DtoReturnDataSuccess<DtoUserGet>> EditProfile([Service] IUser _user, int id, DtoUserEditProfile input)
        {
            // TODO Get UserId from JWT Claim
            var result = await _user.Update(id, _mapper.Map<User>(input));
            return new DtoReturnDataSuccess<DtoUserGet>
            {
                data = _mapper.Map<DtoUserGet>(result)
            };
        }

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

        public async Task<DtoReturnDataSuccess<DtoUserGet>> DeleteUser([Service] IUser _user, int id)
        {
            var result = await _user.Delete(id);

            return new DtoReturnDataSuccess<DtoUserGet>
            {
                data = _mapper.Map<DtoUserGet>(result)
            };
        }


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

        public async Task<DtoReturnDataSuccess<UserRole>> UnassignUserRole([Service] IUserRole _userRole, int userId, int roleId)
        {
            var result = await _userRole.Delete(userId, roleId);

            return new DtoReturnDataSuccess<UserRole>
            {
                data = result
            };
        }
    }
}