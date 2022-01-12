using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaymentServices.Data;
using PaymentServices.Dtos;
using PaymentServices.Exceptions;
using PaymentServices.Models;

namespace PaymentServices.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        private IPayment _payment;
        private HttpClient _httpClient;
        private IConfiguration _config;

        public PaymentController(
            IPayment payment,
            HttpClient httpClient,
            IConfiguration config)
        {
            _payment = payment;
            _httpClient = httpClient;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> AddPayment([FromBody] Payment payment)
        {
            try
            {
                var result = await _payment.Insert(payment);

                return Ok(
                    new DtoReturnDataSuccess
                    {
                        data = result
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

        [HttpGet("{StudentId}")]
        public async Task<ActionResult<DtoPaymentGet>> GetPaymentFromStudentId(int StudentId)
        {
            try
            {
                var paymentData = await _payment.GetByStudentId(StudentId);

                List<DtoCourse> courseList = new List<DtoCourse>();

                var totalCredit = 0;

                var url = _config["Services:Enrollment"];
                using (var message = new HttpRequestMessage(HttpMethod.Get, $"{url}course/"))
                {
                    // TODO add auth token to header

                    var response = await _httpClient.SendAsync(message);
                    var responseData = await response.Content.ReadAsByteArrayAsync();
                    var responseDeserialized = JsonSerializer.Deserialize<DtoReturnDataIEnumerableCourse>(responseData);
                    var responseValue = responseDeserialized.data;

                    foreach (var tes in responseValue)
                    {
                        Console.WriteLine($"{tes.id}, {tes.code}");
                    }

                    foreach (var payment in paymentData)
                    {
                        var course = responseValue.FirstOrDefault(x => x.id == payment.CourseId);
                        if (course == null) throw new DataNotFoundException(payment.CourseId.ToString());
                        courseList.Add(course);

                        totalCredit += course.credit;
                    }

                    return Ok(
                        new DtoReturnDataSuccess
                        {
                            data = new DtoPaymentGet
                            {
                                StudentId = StudentId,
                                Courses = courseList,
                                TotalCredit = totalCredit,
                            }
                        }
                    );
                }
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