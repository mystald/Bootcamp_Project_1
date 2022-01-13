using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentServices.Models;

namespace PaymentServices.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Database.CanConnect()) throw new Exception("Database not migrated yet");

            if (!context.Payments.Any())
            {
                context.Payments.Add(
                    new Payment
                    {
                        EnrollmentId = 1,
                        StudentId = 1,
                        CourseId = 1,
                    }
                );

                context.SaveChanges();

                context.Payments.Add(
                    new Payment
                    {
                        EnrollmentId = 2,
                        StudentId = 1,
                        CourseId = 3,
                    }
                );

                context.SaveChanges();

                context.Payments.Add(
                    new Payment
                    {
                        EnrollmentId = 3,
                        StudentId = 2,
                        CourseId = 3,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}