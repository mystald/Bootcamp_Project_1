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

                var url = _config["Services:Enrollment"];

                var course = new DtoCourse();

                using (var message = new HttpRequestMessage(HttpMethod.Get, $"{url}course/{result.CourseId}"))
                {
                    message.Headers.Add("Content-Type", "application/json");
                    // TODO add auth token to header

                    var response = await _httpClient.SendAsync(message);

                    var responseData = await response.Content.ReadAsStringAsync();

                    var responseValue = JsonSerializer.Deserialize<DtoCourse>(responseData);
                }

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
    }
}