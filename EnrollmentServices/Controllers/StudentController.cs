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
        public async Task<ActionResult<IEnumerable<DtoStudent>>> GetAll()
        {
            try
            {
                var results = await _student.GetAll();

                return Ok(_mapper.Map<IEnumerable<DtoStudent>>(results));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DtoStudent>> GetById(int id)
        {
            try
            {
                var result = await _student.GetById(id);

                return Ok(_mapper.Map<DtoStudent>(result));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DtoStudent>> Add([FromBody] DtoStudentInsert obj)
        {
            try
            {
                var result = await _student.Insert(_mapper.Map<Student>(obj));

                return Ok(_mapper.Map<DtoStudent>(result));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DtoStudent>> Edit(int id, [FromBody] DtoStudentInsert obj)
        {
            try
            {
                var result = await _student.Update(id, _mapper.Map<Student>(obj));

                return Ok(_mapper.Map<DtoStudent>(result));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DtoStudent>> Delete(int id)
        {
            try
            {
                var result = await _student.Delete(id);

                return Ok(_mapper.Map<DtoStudent>(result));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}