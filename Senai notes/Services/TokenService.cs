using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Senai_notes.Services
{
    public class TokenService
    {
            public string GenerateToken(string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minha-chave-ultra-mega-secreta-senai"));

            var creds = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "senaiNotes",
                audience: "senaiNotes",
                claims: claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }  
    }
}
