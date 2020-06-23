using Entities;
using Microsoft.IdentityModel.Tokens;
using Services.Helpers;
using Services.Region;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Services.Tokens
{
    public class TokenServices
    {
        static string jwtKey = Settings.Configurations.JwtKey;

        public static string Create(User user)
        {
            var now = RegionServices.CurrentDateTime();
            var userId = SharedServices.ConvertGuid(user.Id);

            var claims = new[]
                        {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, user.Username)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = now.AddDays(365),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public static User Read(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            var userId = jwtToken.Claims.First(claim => claim.Type == "NameIdentifier").Value;

            var convertedUserId = SharedServices.ConvertToGuid(userId);
            var userName = jwtToken.Claims.First(claim => claim.Type == "Name").Value;

            var user = new User() { Username = userName, Id = convertedUserId };

            return user;
        }

    }
}
