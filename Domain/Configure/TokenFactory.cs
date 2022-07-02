using MinimalLetsApiAuth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MinimalLetsApiAuth.Domain.Interfaces;

namespace MinimalLetsApiAuth.Domain.Configure
{
    public class TokenFactory : ITokenFactory
    {

        private readonly WebApplicationBuilder _configuration; 
        

        public TokenFactory(WebApplicationBuilder configuration)
        {
            _configuration= configuration;  
        }


        public Token GenerateToken(User user)
        {   
            //Getting Key
            var key = Encoding.ASCII.GetBytes(_configuration.Configuration["TokenConfigurations:secureKey"]);
            var issuer = _configuration.Configuration["TokenConfigurations:Issuer"];
            var audience = _configuration.Configuration["TokenConfigurations:Audience"];
            var created = DateTime.UtcNow;
            var Expires = DateTime.UtcNow.AddMinutes(30);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserName.ToString()),
                    new Claim(ClaimTypes.Role,user.Role.Value.ToString())
                }),
                Issuer = issuer,
                Audience= audience,
                Expires = Expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            
            return GetToken(created, Expires, token);
        }


        private static Token GetToken(DateTime dataCriacao, DateTime dataExpiracao, string token)
        {
            return new Token()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }

    }
}
