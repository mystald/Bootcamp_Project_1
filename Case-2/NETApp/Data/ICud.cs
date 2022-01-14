using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETApp.Data
{
    public interface ICud<T>
    {
        Task Insert(T obj);
        Task Update(T newObj);
        Task Delete(T oldObj);
    }
}