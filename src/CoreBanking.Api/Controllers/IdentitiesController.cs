using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Identities;
using CoreBanking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentitiesController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentitiesController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        [Authorize(Policy = "Accessibility")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            await _identityService.RegisterAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authenticates a user")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto dto)
        {
            var result = await _identityService.LoginAsync(dto);
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var user = await _identityService.GetUserAsync(User);
            await _identityService.LogoutAsync(user.Id);
            return Ok();
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var result = await _identityService.RefreshTokenAsync(refreshToken);
            return Ok(new { accessToken = result.AccessToken, refreshToken = result.RefreshToken });
        }
    }
}
