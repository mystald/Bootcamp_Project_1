using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentServices.Data;
using PaymentServices.Dtos;
using PaymentServices.Exceptions;
using PaymentServices.External;
using PaymentServices.Models;

namespace PaymentServices.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        private IPayment _payment;
        private IEnrollmentService _enroll;

        public PaymentController(IPayment payment, IEnrollmentService enroll)
        {
            _payment = payment;
            _enroll = enroll;
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

                var result = await _enroll.GetDetail(paymentData, StudentId);

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