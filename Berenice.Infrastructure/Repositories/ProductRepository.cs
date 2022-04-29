using Berenice.Core.Data;
using Berenice.Core.Dtos;
using Berenice.Core.Interfaces;
using Berenice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Berenice.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BereniceDBContext bereniceDBContext;
        public ProductRepository(BereniceDBContext bereniceDBContext)
        {
            this.bereniceDBContext = bereniceDBContext;
        }
        public async Task<ApiResponse<ProductDTO>> AddProduct(ProductDTO product)
        {
            //usar AuoMapper
            var productDb = new Product
            { 
                Brand = product.Brand,
                Category = product.Category,
                ModelYear = product.ModelYear,
                Price = product.Price,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
            };
            await bereniceDBContext.Products.AddAsync(productDb);
            int result = await bereniceDBContext.SaveChangesAsync();
            if (result > 0)
                product.ProductId = productDb.ProductId;

            return new ApiResponse<ProductDTO>
            {
                Response = (result > 0) ? product : null,
                Message = (result > 0) ? "Correcto" : "Error",
                Status = result > 0
            };

        }
        public async Task<ApiResponse<ProductDTO>> GetProductById(int productId)
        {
            var product = await bereniceDBContext.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (product != null)
            {
                var productDto = new ProductDTO
                {
                    Brand = product.Brand,
                    ProductId = product.ProductId,
                    ProductName= product.ProductName,
                    Price = product.Price,
                    ModelYear= product.ModelYear,
                    Category = product.Category,
                    OrdersCount = bereniceDBContext.Orders.Count(x => x.CustomerId == productId)
                };
                return new ApiResponse<ProductDTO>
                {
                    Response = productDto,
                    Message = "Correcto",
                    Status = true
                };
            }
            else
            {
                return new ApiResponse<ProductDTO>
                {
                    Response = null,
                    Message = "No hay producto",
                    Status = false
                };
            }
        }
        public async Task<ApiResponse<ProductDTO>> UpdateProduct(ProductDTO productDto)
        {
            var product = await bereniceDBContext.Products.Where(x => x.ProductId == productDto.ProductId)
                .FirstOrDefaultAsync();
            if (product != null)
            {
                product.ProductName = productDto.ProductName;
                product.Price = productDto.Price;
                product.Brand = productDto.Brand;
                product.Category = productDto.Category;
                product.ModelYear = productDto.ModelYear;

                int result = await bereniceDBContext.SaveChangesAsync();
                return new ApiResponse<ProductDTO>
                {
                    Response = productDto,
                    Message = (result > 0) ? "Producto actualizado" : "Error",
                    Status = result > 0
                };
            }
            else
            {
                return new ApiResponse<ProductDTO>
                {
                    Status = false,
                    Message = "No se realizo la actualziación",
                    Response = null,
                };
            }
        }
        public async Task<ApiResponse<IEnumerable<ProductDTO>>> GetProducts()
        {
            var productsResponse = await bereniceDBContext.Products.ToListAsync();
            if (productsResponse.Count > 0)
            {
                var listDto = new List<ProductDTO>();
                productsResponse.ForEach(item =>
                {
                    listDto.Add(new ProductDTO
                    {
                        Brand = item.Brand,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Category = item.Category,
                        ModelYear = item.ModelYear,
                        ProductName= item.ProductName,
                        OrdersCount = bereniceDBContext.Orders.Count(x => x.CustomerId == item.ProductId)
                    });
                });
                return new ApiResponse<IEnumerable<ProductDTO>>
                {
                    Response = listDto,
                    Message = string.Empty,
                    Status = true
                };
            }
            else
            {

                return new ApiResponse<IEnumerable<ProductDTO>>
                {
                    Response = null,
                    Message = string.Empty,
                    Status = false
                };
            }
        }
        public async Task<ApiResponse<string>> DeleteProduct(int productId)
        {

            var product = await bereniceDBContext.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (product != null)
            {
                bereniceDBContext.Products.Remove(product);
                int result = await bereniceDBContext.SaveChangesAsync();
                return new ApiResponse<string>
                {
                    Response = string.Empty,
                    Status = result > 0,
                    Message = (result > 0) ? "Producto Eliminado" : "No fue posible Eliminar el producto"
                };
            }
            else
            {

                return new ApiResponse<string>
                {
                    Response = string.Empty,
                    Status = false,
                    Message = "No fue posible Eliminar el producto"
                };
            }
        }
    }
}