using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GraphQLAPI.Exceptions;
using GraphQLAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GraphQLAPI.Data
{
    public class DALUser : IUser
    {
        private ApplicationDbContext _db;
        private IConfiguration _config;

        public DALUser(
            ApplicationDbContext db,
            IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public async Task<string> Authentication(string username, string password)
        {
            var userFound = await (
                from user in _db.Users
                where user.Username == username && user.Password == password
                select user
            ).SingleOrDefaultAsync();

            if (userFound == null) throw new System.Exception("Invalid Credentials");

            IEnumerable<Role> roles = new List<Role>();

            try { roles = await GetRoles(userFound.Id); }
            catch (DataNotFoundException) { }

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("ID", userFound.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, userFound.Username));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<User> Delete(int id)
        {
            try
            {
                var oldUser = await GetById(id);

                _db.Remove(oldUser);

                await _db.SaveChangesAsync();

                return oldUser;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var results = await _db.Users.ToListAsync();
            if (!results.Any()) throw new DataNotFoundException("Users not found");

            return results;
        }

        public async Task<User> GetById(int id)
        {
            var result = await _db.Users.Where(
                user => user.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("User not found");

            return result;
        }

        public async Task<User> GetByUsername(string username)
        {
            var result = await _db.Users.Where(
                user => user.Username == username
            ).SingleOrDefaultAsync();

            if (result == null) throw new DataNotFoundException("Username not found");

            return result;
        }

        public async Task<IEnumerable<Role>> GetRoles(int id)
        {
            var roles = await (
                from user in _db.Users
                join userRole in _db.UserRoles on user.Id equals userRole.UserId
                join role in _db.Roles on userRole.RoleId equals role.Id
                where user.Id == id
                select role
            ).ToListAsync();

            if (!roles.Any()) throw new DataNotFoundException("Roles not found");

            return roles;
        }

        public async Task<IEnumerable<Twittor>> GetTwittors(int id)
        {
            var twittors = await (
                from user in _db.Users
                join twittor in _db.Twittors on user.Id equals twittor.UserId
                where user.Id == id
                select twittor
            ).ToListAsync();

            if (!twittors.Any()) throw new DataNotFoundException("Twittors not found");

            return twittors;
        }

        public async Task<User> Insert(User obj)
        {
            try
            {
                try
                {
                    var userExist = await GetByUsername(obj.Username);

                    throw new System.Exception("Username already taken");
                }
                catch (DataNotFoundException)
                {
                    // do nothing
                }

                var result = await _db.Users.AddAsync(obj);

                await _db.SaveChangesAsync();

                return result.Entity;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<User> Update(int id, User obj)
        {
            try
            {
                var oldUser = await GetById(id);

                oldUser.Username = obj.Username;
                oldUser.Password = obj.Password;
                oldUser.isLocked = obj.isLocked;

                await _db.SaveChangesAsync();

                return oldUser;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}