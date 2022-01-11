using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.Dtos;
using EnrollmentServices.Models;

namespace EnrollmentServices.Data
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<DtoGetEnrollments>> GetEnrollments(int StudentId);
    }
}