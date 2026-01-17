using CentralBankCreditCheckService.Models;
using CentralBankCreditCheckService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CentralBankCreditCheckService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentralBankCreditCheckController : ControllerBase
    {
        private readonly MainService _mainService;
        public CentralBankCreditCheckController(MainService mainService)
        {
            _mainService = mainService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ValidationResult>> GetCentralBankCreditCheck(string id)
        {
            var reslut = await _mainService.GetResultAsync(id.ToString());
            if (reslut == null)
            {
                return NotFound();
            }
            return reslut;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthCheck()
        {
            return Ok();
        }
    }
}
