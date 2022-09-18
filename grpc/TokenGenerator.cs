
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace grpcserv1.Tokens
{
    public class TokenGenerator
    {
        public static string Generate(int ind , string[] Uris,int days=7)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WsetKweKsdfvsdL124.$"));

            var tokenHandler = new JwtSecurityTokenHandler();

            List<Claim> claims = new List<Claim> ()  ;
            claims.Add( new Claim(ClaimTypes.NameIdentifier, ind.ToString())) ;
            foreach(var uri in Uris)
            {
                 claims.Add( new Claim(ClaimTypes.Uri, uri)) ;        
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(days),
                Issuer = "SecondVariety",
                Audience = "Clients",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}