using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceClearanceService.Models;
using PoliceClearanceService.Services;
using System;

namespace PoliceClearanceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliceClearanceController : ControllerBase
    {
        private readonly MainService _mainService;
        public PoliceClearanceController(MainService mainService)
        {
            _mainService = mainService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PoliceClearanceResult>> GetPoliceClearance(string id)
        {
            var result = await _mainService.GetResultAsync(id.ToString());
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}
