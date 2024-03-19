using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.Dtos;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Produces("Application/json")]
    [ApiController]
    [Route("Api/[Controller]")]

    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        public SecurityController(ISecurityService securityRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _securityRepository = securityRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(SecurityDto user)
        {
            var security = _mapper.Map<Security>(user);
            security.Password = _passwordHasher.Hash(security.Password);
            await _securityRepository.RegisterUser(security);
            var securityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);
            return Ok(response);
        }
    }
}
