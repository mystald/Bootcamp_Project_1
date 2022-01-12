using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnrollmentServices.Dtos
{
    public class DtoStudentGet
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class DtoStudentInsert
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
    }

    public class DtoStudentEnrollsGet
    {
        public string Code { get; set; }
        public string CourseName { get; set; }
        public int? Credit { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Grade { get; set; }
    }
}