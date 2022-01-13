using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Data;
using AuthServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServices.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ServicesController : ControllerBase
    {
        private IServices _serv;

        public ServicesController(IServices services)
        {
            _serv = services;
        }
        [HttpGet]
        public ActionResult<DtoReturnDataSuccess<DtoUserAuthResult>> GetToken()
        {
            try
            {
                return Ok(new DtoReturnDataSuccess<DtoUserAuthResult>
                {
                    data = new DtoUserAuthResult
                    {
                        token = _serv.GenerateToken()
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