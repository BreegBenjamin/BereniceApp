using Berenice.Core.Dtos;

namespace Berenice.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<ApiResponse<ProductDTO>> AddProduct(ProductDTO product);
        Task<ApiResponse<ProductDTO>> GetProductById(int productId);
        Task<ApiResponse<ProductDTO>> UpdateProduct(ProductDTO productDto);
        Task<ApiResponse<string>> DeleteProduct(int productId);
        Task<ApiResponse<IEnumerable<ProductDTO>>> GetProducts();
    }
}
