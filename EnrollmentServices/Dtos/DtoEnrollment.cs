using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentServices.Models;

namespace EnrollmentServices.Dtos
{
    public class DtoEnrollmentGet
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Grade { get; set; }
    }

    public class DtoEnrollmentGetDetail
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Grade { get; set; }
    }

    public class DtoEnrollmentInsert
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public Grade Grade { get; set; }
    }
}