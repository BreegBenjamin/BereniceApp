using Berenice.Core.Dtos;

namespace Berenice.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<ApiResponse<OrderDTO>> AddOrder(OrderDTO product);
        Task<ApiResponse<OrderDTO>> GetOrderById(int orderId);
        Task<ApiResponse<OrderDTO>> UpdateOrder(OrderDTO orderDTO);
        Task<ApiResponse<IEnumerable<OrderDTO>>> GetOrders();
        Task<ApiResponse<string>> DeleteOrder(int orderId);
    }
}
