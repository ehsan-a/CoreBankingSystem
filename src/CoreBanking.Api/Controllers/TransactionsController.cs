using AutoMapper;
using CoreBanking.Application.DTOs.Requests.Transaction;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Accessibility")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetTransactions(CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.GetAllAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<TransactionResponseDto>>(transaction));
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionResponseDto>> GetTransaction(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.GetByIdAsync(id, cancellationToken);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TransactionResponseDto>(transaction));
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionResponseDto>> PostTransaction(CreateTransactionRequestDto createTransactionRequestDto, [FromHeader(Name = "Idempotency-Key")] string idempotencyKey, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(idempotencyKey))
                return BadRequest("Idempotency-Key is required");

            var transaction = await _transactionService.CreateAsync(createTransactionRequestDto, User, idempotencyKey, cancellationToken);

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, _mapper.Map<TransactionResponseDto>(transaction));
        }
    }
}
