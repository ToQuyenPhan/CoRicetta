using CoRicetta.Data.ViewModels.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.JWT
{
    public class JWTUserToken
    {
        public static string GenerateJWTTokenUser(UserTokenViewModel user)
        {
            JwtSecurityToken tokenUser = null;
            tokenUser = new JwtSecurityToken(
                issuer: "https://coricetta.com",
                audience: "https://coricetta.com",
                claims: new[] {
                 //Id
                 new Claim("Id", user.Id.ToString()),
                 //Username
                 new Claim("Username", user.UserName),
                 //Email
                 new Claim("Email", user.Email),
                 //Role
                 new Claim ("Role", user.Role),
                 //Status
                 new Claim("Status", user.Status.ToString()),
                },
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(
                        key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CoRicetta2023ForFPTU")),
                        algorithm: SecurityAlgorithms.HmacSha256
                        )
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenUser);
        }
    }
}
