using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentServices.External.Dtos
{
    public class DtoPayment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
    }
}