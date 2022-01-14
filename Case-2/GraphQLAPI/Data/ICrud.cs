using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Data
{
    public interface ICrud<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T obj);
        Task<T> Update(int id, T obj);
        Task<T> Delete(int id);
    }
}