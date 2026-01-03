using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Accessibility")]
        public async Task<ActionResult<AuthenticationResponseDto>> GetAuthentications(string id, CancellationToken cancellationToken)
        {
            return Ok(await _authenticationService.GetInquiryAsync(id, cancellationToken));
        }

        [HttpPost]
        [Authorize(Policy = "Accessibility")]
        public async Task<ActionResult<RegisteredAuthResponseDto>> PostAuthentications(CreateAuthenticationRequestDto createAuthenticationRequestDto, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.CreateAsync(createAuthenticationRequestDto, User, cancellationToken);

            return CreatedAtAction("GetAuthentications", new { id = createAuthenticationRequestDto.NationalCode }, result);
        }
    }
}
