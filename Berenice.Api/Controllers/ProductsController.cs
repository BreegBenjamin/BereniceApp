using Berenice.Core.Dtos;
using Berenice.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Berenice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetProducts([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest("Id no puede ser 0");
            }
            var product = await productRepository.GetProductById(id);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductDTO productDTO)
        {
            var result = await productRepository.AddProduct(productDTO);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO)
        {
            if (productDTO.ProductId == 0)
            {
                return BadRequest("El Id del producto no puede ser 0");
            }
            var result = await productRepository.UpdateProduct(productDTO);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id no puede ser 0");
            }
            var response = await productRepository.DeleteProduct(id);
            return Ok(response);
        }
    }
}
