using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Data;
using AuthServices.Dtos;
using AuthServices.Helpers;
using AuthServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthServices.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost("auth")]
        public async Task<ActionResult<DtoUserAuthResult>> AuthUser([FromBody] DtoUserAuthInput input)
        {
            try
            {
                var result = await _user.Authenticate(input.username, Hash.getHash(input.password));

                return Ok(new DtoReturnDataSuccess
                {
                    data = new DtoUserAuthResult
                    {
                        token = result,
                    }
                });
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