using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentServices.Dtos
{
    public class DtoPaymentGet
    {
        public int StudentId { get; set; }
        //public DtoStudent Student { get; set; }
        public IEnumerable<DtoCourse> Courses { get; set; }
        public int TotalCredit { get; set; }
    }

    public class DtoStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class DtoCourse
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int credit { get; set; }
    }
}