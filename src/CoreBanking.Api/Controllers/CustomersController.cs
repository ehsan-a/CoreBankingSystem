using CoreBanking.Application.DTOs.Requests.Customer;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Accessibility")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetCustomers(CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetAllAsync(cancellationToken);
            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomer(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetByIdAsync(id, cancellationToken);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, UpdateCustomerRequestDto updateCustomerRequestDto, CancellationToken cancellationToken)
        {
            if (id != updateCustomerRequestDto.Id)
            {
                return BadRequest();
            }

            try
            {
                //var customer = await _customerService.GetByIdAsync(id, cancellationToken);
                await _customerService.UpdateAsync(updateCustomerRequestDto, User, cancellationToken);
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
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<CustomerResponseDto>> PostCustomer(CreateCustomerRequestDto createCustomerRequestDto, CancellationToken cancellationToken)
        {

            var customer = await _customerService.CreateAsync(createCustomerRequestDto, User, cancellationToken);
            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
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

            await _customerService.DeleteAsync(customer.Id, User, cancellationToken);

            return NoContent();
        }

        private async Task<bool> CustomerExists(Guid id, CancellationToken cancellationToken)
        {
            return await _customerService.ExistsAsync(id, cancellationToken);
        }
    }
}
