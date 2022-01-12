using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentServices.Exceptions;
using PaymentServices.Models;

namespace PaymentServices.Data
{
    public class DALPayment : IPayment
    {
        private ApplicationDbContext _db;
        private HttpClient _httpClient;

        public DALPayment(ApplicationDbContext db, HttpClient httpClient)
        {
            _db = db;
            _httpClient = httpClient;
        }

        public Task<Payment> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Payment>> GetByStudentId(int StudentId)
        {
            var results = await _db.Payments.Where(
                payment => payment.StudentId == StudentId
            ).ToListAsync();

            if (!results.Any()) throw new DataNotFoundException("Payments not found");

            return results;
        }

        public async Task<Payment> Insert(Payment obj)
        {
            try
            {
                var result = await _db.AddAsync(obj);

                await _db.SaveChangesAsync();

                return result.Entity;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<Payment> Update(int id, Payment obj)
        {
            throw new NotImplementedException();
        }
    }
}