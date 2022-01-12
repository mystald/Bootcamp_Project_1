using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AuthServices.Dtos
{
    public class DtoReturnDataSuccess<T>
    {
        public string status { get; set; } = Status.success.ToString();
        public T data { get; set; }
    }

    public class DtoReturnDataError
    {
        public string status { get; set; } = Status.error.ToString();
        public string message { get; set; }
    }

    public enum Status
    {
        success,
        error
    }
}