using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentServices.Models;

namespace PaymentServices.Data
{
    public interface IPayment : ICrud<Payment>
    {
        Task<IEnumerable<Payment>> GetByStudentId(int StudentId);
    }
}