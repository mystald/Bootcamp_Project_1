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
    public class CourseController : ControllerBase
    {
        private ICourse _course;
        private IMapper _mapper;

        public CourseController(ICourse course, IMapper mapper)
        {
            _course = course;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoCourseGet>>> GetAll()
        {
            try
            {
                var results = await _course.GetAll();

                return Ok(
                    new DtoReturnDataSuccess
                    {
                        data = _mapper.Map<IEnumerable<DtoCourseGet>>(results)
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
        public async Task<ActionResult<DtoCourseGet>> GetById(int id)
        {
            try
            {
                var result = await _course.GetById(id);

                return Ok(
                    new DtoReturnDataSuccess
                    {
                        data = _mapper.Map<DtoCourseGet>(result)
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
        public async Task<ActionResult<DtoCourseGet>> Add([FromBody] DtoCourseInsert obj)
        {
            try
            {
                var result = await _course.Insert(_mapper.Map<Course>(obj));

                return Ok(_mapper.Map<DtoCourseGet>(result));
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
        public async Task<ActionResult<DtoCourseGet>> Edit(int id, [FromBody] DtoCourseInsert obj)
        {
            try
            {
                var result = await _course.Update(id, _mapper.Map<Course>(obj));

                return Ok(_mapper.Map<DtoCourseGet>(result));
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
        public async Task<ActionResult<DtoCourseGet>> Delete(int id)
        {
            try
            {
                var result = await _course.Delete(id);

                return Ok(_mapper.Map<DtoCourseGet>(result));
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