using AutoMapper;
using CoreBanking.Application.DTOs.Requests.Customer;
using CoreBanking.Application.DTOs.Responses.Customer;
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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetCustomers(CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetAllAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<CustomerResponseDto>>(customers));
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

            return Ok(_mapper.Map<CustomerResponseDto>(customer));
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
                var customer = await _customerService.GetByIdAsync(id, cancellationToken);
                await _customerService.UpdateAsync(_mapper.Map(updateCustomerRequestDto, customer), cancellationToken);
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
            var customer = _mapper.Map<Customer>(createCustomerRequestDto);
            await _customerService.CreateAsync(customer, cancellationToken);
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

            await _customerService.DeleteAsync(customer.Id, cancellationToken);

            return NoContent();
        }

        private async Task<bool> CustomerExists(Guid id, CancellationToken cancellationToken)
        {
            return await _customerService.ExistsAsync(id, cancellationToken);
        }
    }
}
