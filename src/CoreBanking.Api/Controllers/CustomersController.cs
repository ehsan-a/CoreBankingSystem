using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreBanking.Domain.Entities;
using CoreBanking.Application.Interfaces;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;

        public CustomersController(ICustomerService customerService, IAuthenticationService authenticationService)
        {
            _customerService = customerService;
            _authenticationService = authenticationService;
        }



        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(CancellationToken cancellationToken)
        {
            return (await _customerService.GetAllAsync(cancellationToken)).ToList();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetByIdAsync(id, cancellationToken);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, Customer customer, CancellationToken cancellationToken)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            try
            {
                await _customerService.UpdateAsync(customer, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExists(id, cancellationToken))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer, CancellationToken cancellationToken)
        {
            var authenticationResult = await _authenticationService.GetByNationalCodeAsync(customer.NationalCode, cancellationToken);
            if (authenticationResult == null)
                return Unauthorized(new { message = "Customer Authentication failed." });
            if (authenticationResult.CentralBankCreditCheckPassed && authenticationResult.CivilRegistryVerified && authenticationResult.PoliceClearancePassed)
            {
                await _customerService.CreateAsync(customer, cancellationToken);
                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }
            else return BadRequest();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetByIdAsync(id, cancellationToken);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.DeleteAsync(customer.Id, cancellationToken);

            return NoContent();
        }

        private async Task<bool> CustomerExists(Guid id, CancellationToken cancellationToken)
        {
            return await _customerService.ExistsAsync(id, cancellationToken);
        }
    }
}
