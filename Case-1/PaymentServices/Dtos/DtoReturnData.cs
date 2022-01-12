using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaymentServices.Dtos
{
    public class DtoReturnDataSuccess
    {
        public string status { get; set; } = Status.success.ToString();
        public Object data { get; set; }
    }

    public class DtoReturnDataError
    {
        public string status { get; set; } = Status.error.ToString();
        public string message { get; set; }
    }

    public class DtoReturnDataIEnumerableCourse
    {
        public string status { get; set; } = Status.error.ToString();
        public IEnumerable<DtoCourse> data { get; set; }
        public string message { get; set; }
    }

    public enum Status
    {
        success,
        error
    }
}