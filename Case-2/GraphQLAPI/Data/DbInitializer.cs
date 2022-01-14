using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLAPI.Helpers;
using GraphQLAPI.Models;

namespace GraphQLAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                context.Users.Add(
                    new User
                    {
                        Username = "hilman",
                        Password = Hash.getHash("password")
                    }
                );

                context.SaveChanges();

                context.Users.Add(
                    new User
                    {
                        Username = "abdan",
                        Password = Hash.getHash("password")
                    }
                );

                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                context.Roles.Add(
                    new Role
                    {
                        Name = "ADMIN"
                    }
                );

                context.SaveChanges();

                context.Roles.Add(
                    new Role
                    {
                        Name = "MEMBER"
                    }
                );

                context.SaveChanges();
            }

            if (!context.UserRoles.Any())
            {
                context.UserRoles.Add(
                    new UserRole
                    {
                        UserId = 1,
                        RoleId = 1,
                    }
                );

                context.UserRoles.Add(
                    new UserRole
                    {
                        UserId = 2,
                        RoleId = 2,
                    }
                );
            }

            context.SaveChanges();

            if (!context.Twittors.Any())
            {
                context.Twittors.Add(
                    new Twittor
                    {
                        UserId = 1,
                        Content = "Hello Twittor!!",
                        PostDate = DateTime.Now
                    }
                );
            }

            context.SaveChanges();

            if (!context.Comments.Any())
            {
                context.Comments.Add(
                    new Comment
                    {
                        UserId = 2,
                        TwittorId = 1,
                        Content = "Wih mantappp",
                        PostDate = DateTime.Now.AddMinutes(10),
                    }
                );
            }

            context.SaveChanges();
        }
    }
}