using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentServices.Dtos
{
    public class DtoPaymentGet
    {
        public int Id { get; set; }
        public DtoStudent Student { get; set; }
        public DtoCourse Course { get; set; }
        public DtoEnrollment Enrollment { get; set; }
    }

    public class DtoStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class DtoCourse
    {
        public int CourseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
    }

    public class DtoEnrollment
    {
        public int EnrollmentId { get; set; }
        public DateTime EnrollDate { get; set; }
        public char Grade { get; set; }
    }
}