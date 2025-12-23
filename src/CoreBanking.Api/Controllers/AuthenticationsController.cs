using AutoMapper;
using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenService _tokenService;

        public AuthenticationsController(IAuthenticationService authenticationService, IIdentityService identityService, IJwtTokenService tokenService)
        {
            _authenticationService = authenticationService;
            _identityService = identityService;
            _tokenService = tokenService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthenticationResponseDto>> GetAuthentications(string id, CancellationToken cancellationToken)
        {
            return Ok(await _authenticationService.GetInquiryAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponseDto>> PostAuthentications(CreateAuthenticationRequestDto createAuthenticationRequestDto, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.CreateAsync(createAuthenticationRequestDto, cancellationToken);

            return CreatedAtAction("GetAuthentications", new { id = createAuthenticationRequestDto.NationalCode }, result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            await _identityService.RegisterAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            await _identityService.LoginAsync(dto);
            var token = _tokenService.GenerateToken(await _identityService.GetUserAsync(User));
            return Ok(new { token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();
            return Ok();
        }

    }
}
