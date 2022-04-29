using Berenice.Core.Dtos;
using Berenice.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Berenice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        public OrdersController(IOrderRepository orderRepository) 
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Getorders()
        {
            var customer = await orderRepository.GetOrders();
            return Ok(customer);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetOrderById([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest("Id no puede ser 0");
            }
            var customer = await orderRepository.GetOrderById(id);
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderDTO orderDTO)
        {
            var result = await orderRepository.AddOrder(orderDTO);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderDTO orderDTO)
        {
            if (orderDTO.OrderId == 0)
            {
                return BadRequest("El Id del Cliente no puede ser 0");
            }
            var result = await orderRepository.UpdateOrder(orderDTO);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id no puede ser 0");
            }
            var response = await orderRepository.DeleteOrder(id);
            return Ok(response);
        }

    }
}
