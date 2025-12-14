using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts(CancellationToken cancellationToken)
        {
            return (await _accountService.GetAllAsync(cancellationToken)).ToList();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(Guid id, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetByIdAsync(id, cancellationToken);


            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, Account account, CancellationToken cancellationToken)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }
            try
            {
                await _accountService.UpdateAsync(account, cancellationToken);
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
        public async Task<ActionResult<Account>> PostAccount(Account account, CancellationToken cancellationToken)
        {
            await _accountService.CreateAsync(account, cancellationToken);

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
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

            await _accountService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }

        private async Task<bool> AccountExists(Guid id, CancellationToken cancellationToken)
        {
            return await _accountService.ExistsAsync(id, cancellationToken);
        }
    }
}
