using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Api.Controllers
{
    [Produces("Application/json")]
    [ApiController]
    [Route("Api/[Controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private bool IsValidUser(UserLogin user)
        {
            return true;
        }
        private string GenerateToken()
        {
            var simetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(simetricSecuritykey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Clients
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"Jhonny la bestia"),
                new Claim(ClaimTypes.Email,"alantubert@gmail.com"),
                new Claim(ClaimTypes.Role,"Administrador")
            };
            //Payload 
            var payload = new JwtPayload(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(5));
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        public IActionResult GetToken(UserLogin user)
        {
            if(!IsValidUser(user))
            {
                return NotFound(new
                {
                    message = "user unathorized"
                });
            }
            var token = GenerateToken();
            return Ok(new
            {
                token
            });
        }
    }
}
