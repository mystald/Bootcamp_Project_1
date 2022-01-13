using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthServices.Data
{
    public class DALServices : IServices
    {
        private IConfiguration _config;

        public DALServices(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken()
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "Services"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMonths(6),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            //userFound.Token = tokenHandler.WriteToken(token);

            //Console.WriteLine(newUser.Token);

            return tokenHandler.WriteToken(token);
        }
    }
}