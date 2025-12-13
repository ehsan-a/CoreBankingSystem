using CoreBanking.Application.DTOs;
using CoreBanking.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<ActionResult<AuthenticationResponseDto>> GetAuthentications(string id)
        {
            var person = await _authenticationService.GetCivilRegistryAsync(id);
            if (person == null) return NotFound();
            var result = new AuthenticationResponseDto
            {
                civilRegistry = await _authenticationService.GetCivilRegistryAsync(id),
                centralBankCreditCheck = await _authenticationService.GetCentralBankCreditCheckAsync(id),
                policeClearance = await _authenticationService.GetPoliceClearanceAsync(id),
            };
            return result;
        }

    }
}
