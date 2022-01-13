using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServices.Data
{
    public interface IServices
    {
        string GenerateToken();
    }
}