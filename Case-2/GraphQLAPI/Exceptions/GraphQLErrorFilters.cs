using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;

namespace GraphQLAPI.Exceptions
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception == null) return error.WithMessage(error.Message);

            return error.WithMessage(error.Exception.Message);
        }
    }
}