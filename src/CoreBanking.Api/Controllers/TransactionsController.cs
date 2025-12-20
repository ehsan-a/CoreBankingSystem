using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(CancellationToken cancellationToken)
        {
            return (await _transactionService.GetAllAsync(cancellationToken)).ToList();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.GetByIdAsync(id, cancellationToken);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(Guid id, Transaction transaction, CancellationToken cancellationToken)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }
            try
            {
                await _transactionService.UpdateAsync(transaction, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TransactionExists(id, cancellationToken))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction, CancellationToken cancellationToken)
        {
            await _transactionService.CreateAsync(transaction, cancellationToken);

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.GetByIdAsync(id, cancellationToken);
            if (transaction == null)
            {
                return NotFound();
            }

            await _transactionService.DeleteAsync(transaction.Id, cancellationToken);

            return NoContent();
        }

        private async Task<bool> TransactionExists(Guid id, CancellationToken cancellationToken)
        {
            return await _transactionService.ExistsAsync(id, cancellationToken);
        }
    }
}
