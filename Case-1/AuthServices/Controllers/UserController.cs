using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Data;
using AuthServices.Dtos;
using AuthServices.Exceptions;
using AuthServices.Helpers;
using AuthServices.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthServices.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private IUser _user;
        private IRole _role;
        private IUserRole _userRole;
        private IMapper _mapper;

        public UserController(IUser user, IRole role, IUserRole userRole, IMapper mapper)
        {
            _user = user;
            _role = role;
            _userRole = userRole;
            _mapper = mapper;
        }

        [HttpPost("auth")]
        public async Task<ActionResult<DtoReturnDataSuccess<DtoUserAuthResult>>> AuthUser([FromBody] DtoUserAuthInput input)
        {
            try
            {
                var result = await _user.Authenticate(input.username, Hash.getHash(input.password));

                return Ok(new DtoReturnDataSuccess<DtoUserAuthResult>
                {
                    data = new DtoUserAuthResult
                    {
                        token = result,
                    }
                });
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(new DtoReturnDataError
                {
                    message = ex.Message,
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<DtoReturnDataSuccess<IEnumerable<DtoUserGet>>>> GetAll()
        {
            try
            {
                var result = await _user.GetAll();

                return Ok(new DtoReturnDataSuccess<IEnumerable<DtoUserGet>>
                {
                    data = _mapper.Map<IEnumerable<DtoUserGet>>(result)
                });
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(new DtoReturnDataError
                {
                    message = ex.Message,
                });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<DtoUserRegisterOutput>> Register([FromBody] DtoUserAdminRegisterInput input)
        {
            try
            {
                var result = await _user.Insert(_mapper.Map<User>(input));

                return Ok(new DtoReturnDataSuccess<DtoUserRegisterOutput>
                {
                    data = new DtoUserRegisterOutput
                    {
                        Username = result.Username
                    }
                });
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(new DtoReturnDataError
                {
                    message = ex.Message,
                });
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<DtoReturnDataSuccess<DtoUserGet>>> GetByUserId(int userId)
        {
            try
            {
                var result = await _user.GetById(userId);

                return Ok(new DtoReturnDataSuccess<DtoUserGet>
                {
                    data = _mapper.Map<DtoUserGet>(result)
                });
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(new DtoReturnDataError
                {
                    message = ex.Message,
                });
            }
        }

        [HttpGet("{userId}/role")]
        public async Task<ActionResult<DtoReturnDataSuccess<IEnumerable<DtoRoleGet>>>> GetRoles(int userId)
        {
            try
            {
                var result = await _user.GetRoles(userId);

                return Ok(new DtoReturnDataSuccess<IEnumerable<DtoRoleGet>>
                {
                    data = _mapper.Map<IEnumerable<DtoRoleGet>>(result)
                });
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(new DtoReturnDataError
                {
                    message = ex.Message,
                });
            }
        }

        [HttpPost("{userId}/role/assign")]
        public async Task<ActionResult<DtoReturnDataSuccess<DtoUserRoleAssignOutput>>> AssignRole(int userId, [FromBody] DtoUserRoleAssignInput input)
        {
            try
            {
                var role = await _role.GetById(input.roleId);
                var user = await _user.GetById(userId);

                var result = await _userRole.Insert(
                    new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                    }
                );

                return Ok(new DtoReturnDataSuccess<DtoUserRoleAssignOutput>
                {
                    data = new DtoUserRoleAssignOutput
                    {
                        Username = user.Username,
                        RoleName = role.Name,
                    }
                });
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(new DtoReturnDataError
                {
                    message = ex.Message,
                });
            }
        }

        [HttpPost("{userId}/role/unassign")]
        public async Task<ActionResult<DtoReturnDataSuccess<DtoUserRoleAssignOutput>>> UnassignRole(int userId, [FromBody] DtoUserRoleAssignInput input)
        {
            try
            {
                var role = await _role.GetById(input.roleId);
                var user = await _user.GetById(userId);

                var userRoleFound = await _userRole.GetByUserIdRoleId(user.Id, role.Id);

                var result = await _userRole.Delete(userRoleFound.Id);

                return Ok(new DtoReturnDataSuccess<DtoUserRoleAssignOutput>
                {
                    data = new DtoUserRoleAssignOutput
                    {
                        Username = user.Username,
                        RoleName = role.Name,
                    }
                });
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(new DtoReturnDataError
                {
                    message = ex.Message,
                });
            }
        }
    }
}