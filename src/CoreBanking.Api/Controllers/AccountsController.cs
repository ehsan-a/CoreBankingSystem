using AutoMapper;
using CoreBanking.Application.DTOs.Requests.Account;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Accessibility")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountResponseDto>>> GetAccounts(CancellationToken cancellationToken)
        {
            var accounts = await _accountService.GetAllAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<AccountResponseDto>>(accounts));
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountResponseDto>> GetAccount(Guid id, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetByIdAsync(id, cancellationToken);


            if (account == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountResponseDto>(account));
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, UpdateAccountRequestDto updateAccountRequestDto, CancellationToken cancellationToken)
        {
            if (id != updateAccountRequestDto.Id)
            {
                return BadRequest();
            }
            try
            {
                var account = await _accountService.GetByIdAsync(id, cancellationToken);
                await _accountService.UpdateAsync(_mapper.Map(updateAccountRequestDto, account), User, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AccountExists(id, cancellationToken))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<AccountResponseDto>> PostAccount(CreateAccountRequestDto createAccountRequestDto, CancellationToken cancellationToken)
        {
            var account = _mapper.Map<Account>(createAccountRequestDto);
            await _accountService.CreateAsync(account, User, cancellationToken);

            return CreatedAtAction("GetAccount", new { id = account.Id }, _mapper.Map<AccountResponseDto>(account));
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetByIdAsync(id, cancellationToken);
            if (account == null)
            {
                return NotFound();
            }

            await _accountService.DeleteAsync(id, User, cancellationToken);

            return NoContent();
        }

        private async Task<bool> AccountExists(Guid id, CancellationToken cancellationToken)
        {
            return await _accountService.ExistsAsync(id, cancellationToken);
        }
    }
}
