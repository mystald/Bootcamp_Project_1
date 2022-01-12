using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.External.Dtos;

namespace EnrollmentServices.External
{
    public interface IPaymentService
    {
        Task AddPayment(DtoPayment payment);
    }
}