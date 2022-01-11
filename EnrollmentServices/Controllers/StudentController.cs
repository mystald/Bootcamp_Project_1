using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.Data;
using EnrollmentServices.Dtos;
using Microsoft.AspNetCore.Mvc;
using EnrollmentServices.Exceptions;
using EnrollmentServices.Models;

namespace EnrollmentServices.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentController : ControllerBase
    {
        private IStudent _student;
        private IMapper _mapper;

        public StudentController(IStudent student, IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoStudentGet>>> GetAll()
        {
            try
            {
                var results = await _student.GetAll();

                return Ok(
                    new DtoReturnDataSuccess
                    {
                        data = _mapper.Map<IEnumerable<DtoStudentGet>>(results)
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
        public async Task<ActionResult<DtoStudentGet>> GetById(int id)
        {
            try
            {
                var result = await _student.GetById(id);

                return Ok(
                    new DtoReturnDataSuccess
                    {
                        data = _mapper.Map<DtoStudentGet>(result)
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
        public async Task<ActionResult<DtoStudentGet>> Add([FromBody] DtoStudentInsert obj)
        {
            try
            {
                var result = await _student.Insert(_mapper.Map<Student>(obj));

                return Ok(_mapper.Map<DtoStudentGet>(result));
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
        public async Task<ActionResult<DtoStudentGet>> Edit(int id, [FromBody] DtoStudentInsert obj)
        {
            try
            {
                var result = await _student.Update(id, _mapper.Map<Student>(obj));

                return Ok(_mapper.Map<DtoStudentGet>(result));
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
        public async Task<ActionResult<DtoStudentGet>> Delete(int id)
        {
            try
            {
                var result = await _student.Delete(id);

                return Ok(_mapper.Map<DtoStudentGet>(result));
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