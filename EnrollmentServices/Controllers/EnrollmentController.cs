using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnrollmentServices.Data;
using EnrollmentServices.Dtos;
using EnrollmentServices.Exceptions;
using EnrollmentServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentServices.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private IEnrollment _enroll;
        private IMapper _mapper;

        public EnrollmentController(IEnrollment enroll, IMapper mapper)
        {
            _enroll = enroll;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoEnrollmentGet>>> GetAll()
        {
            try
            {
                var results = await _enroll.GetAll();

                return Ok(
                    new DtoReturnDataSuccess
                    {
                        data = _mapper.Map<IEnumerable<DtoEnrollmentGet>>(results)
                    }
                );
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
                return BadRequest(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DtoEnrollmentGet>> GetById(int id)
        {
            try
            {
                var result = await _enroll.GetById(id);

                return Ok(
                    new DtoReturnDataSuccess
                    {
                        data = _mapper.Map<DtoEnrollmentGet>(result)
                    }
                );
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
                return BadRequest(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult<DtoEnrollmentGet>> Add([FromBody] DtoEnrollmentInsert obj)
        {
            try
            {
                var result = await _enroll.Insert(_mapper.Map<Enrollment>(obj));

                // Create HttpRequest to PaymentServices

                return Ok(_mapper.Map<DtoEnrollmentGet>(result));
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
                return BadRequest(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DtoEnrollmentGet>> Edit(int id, [FromBody] DtoEnrollmentInsert obj)
        {
            try
            {
                var result = await _enroll.Update(id, _mapper.Map<Enrollment>(obj));

                return Ok(_mapper.Map<DtoEnrollmentGet>(result));
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
                return BadRequest(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DtoEnrollmentGet>> Delete(int id)
        {
            try
            {
                var result = await _enroll.Delete(id);

                return Ok(_mapper.Map<DtoEnrollmentGet>(result));
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
                return BadRequest(
                    new DtoReturnDataError
                    {
                        message = ex.Message
                    }
                );
            }
        }
    }
}