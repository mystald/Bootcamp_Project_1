using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.Models;

namespace EnrollmentServices.Dtos
{
    public class DtoGetStudentEnrolls
    {
        public string Code { get; set; }
        public string CourseName { get; set; }
        public int? Credit { get; set; }
        public DateTime EnrollDate { get; set; }
        public Grade Grade { get; set; }
    }
}