using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Data;
using AuthServices.Dtos;
using AuthServices.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthServices.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private IRole _role;
        private IMapper _mapper;

        public RoleController(IRole role, IMapper mapper)
        {
            _role = role;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DtoReturnDataSuccess<IEnumerable<DtoRoleGet>>>> GetAll()
        {
            try
            {
                var result = await _role.GetAll();

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
    }
}