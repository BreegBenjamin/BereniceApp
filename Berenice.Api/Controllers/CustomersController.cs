using Berenice.Core.Dtos;
using Berenice.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Berenice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;
        public CustomersController(ICustomersRepository customersRepository) 
        {
            this.customersRepository = customersRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var customer = await customersRepository.GetCustomers();
            return Ok(customer);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetCustomer([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest("Id no puede ser 0");
            }
            var customer = await customersRepository.GetCustomerById(id);
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerDTO customerDTO) 
        {
            var result = await customersRepository.AddCustomer(customerDTO);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO customerDTO)
        {
            if (customerDTO.CustomerId == 0) 
            {
                return BadRequest("El Id del Cliente no puede ser 0");
            }
            var result = await customersRepository.UpdateCustomer(customerDTO);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id) 
        {
            if (id == 0) 
            {
                return BadRequest("Id no puede ser 0");
            }
            var response = await customersRepository.DeleteCustomer(id);
            return Ok(response);
        }
    }
}
