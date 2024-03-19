using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Interfaces;
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
        private readonly ISecurityService _securityRepository;
        private readonly IPasswordHasher _passwordHasher;
        public TokenController(IConfiguration configuration,
            ISecurityService securityRepository,
            IPasswordHasher passwordHasher
            )
        {
            _configuration = configuration;
            _securityRepository = securityRepository;
            _passwordHasher = passwordHasher;
        }

        private async Task<(bool, Security)> IsValidUser(UserLogin login)
        {
     
            Security user = await _securityRepository.LoginByCredentials(login);

            var isValid = _passwordHasher.Check(user.Password, login.Password);
            return (isValid, user)!;
        }
        private string GenerateToken(Security security)
        {
            var simetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(simetricSecuritykey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Clients
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, security.UserName),
                new Claim("User",security.User),
                new Claim(ClaimTypes.Role, security.Rol.ToString())
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
        public async Task<IActionResult> GetToken(UserLogin user)
        {
            var validation = await IsValidUser(user);
            if(!validation.Item1)
            {
                return NotFound(new
                {
                    message = "user unathorized"
                });
            }

            var token = GenerateToken(validation.Item2);
            return Ok(new
            {
                token
            });
        }
     
    }
}
