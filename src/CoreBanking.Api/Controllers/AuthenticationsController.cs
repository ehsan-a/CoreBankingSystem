using CoreBanking.Application.DTOs;
using CoreBanking.Application.Interfaces;
using Humanizer;
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
            var person = await _authenticationService.GetCivilRegistryAsync(id);
            if (person == null) return NotFound();
            var result = new AuthenticationResponseDto
            {
                civilRegistry = await _authenticationService.GetCivilRegistryAsync(id),
                centralBankCreditCheck = await _authenticationService.GetCentralBankCreditCheckAsync(id),
                policeClearance = await _authenticationService.GetPoliceClearanceAsync(id),
                RegisteredAuthentication = await _authenticationService.GetByNationalCodeAsync(id, cancellationToken)
            };
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponseDto>> PostAuthentications(AuthenticationResponseDto authenticationResponse, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.CreateAsync(authenticationResponse, cancellationToken);

            return CreatedAtAction("GetAuthentications", new { id = authenticationResponse.civilRegistry.NationalCode }, result);
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
