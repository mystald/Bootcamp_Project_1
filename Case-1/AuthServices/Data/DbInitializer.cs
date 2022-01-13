using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServices.Helpers;
using AuthServices.Models;

namespace AuthServices.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Database.CanConnect()) throw new Exception("Database not migrated yet");

            var role = new Role();

            if (!context.Roles.Any())
            {
                role = context.Roles.Add(new Role
                {
                    Name = "Admin"
                }).Entity;

                context.Roles.Add(new Role
                {
                    Name = "Student"
                });
            }

            context.SaveChanges();

            var user = new User();
            if (!context.Users.Any())
            {
                user = context.Users.Add(new User
                {
                    Username = "Admin",
                    Password = Hash.getHash("password"),
                }).Entity;
            }

            context.SaveChanges();

            if (!context.UserRoles.Any())
            {
                context.UserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id,
                });
            }

            context.SaveChanges();
        }
    }
}