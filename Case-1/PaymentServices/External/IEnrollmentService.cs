using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentServices.Dtos;
using PaymentServices.Models;

namespace PaymentServices.External
{
    public interface IEnrollmentService
    {
        Task<DtoPaymentGet> GetDetail(IEnumerable<Payment> payments, int StudentId);
    }
}