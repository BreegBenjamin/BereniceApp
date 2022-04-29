using Berenice.Core.Data;
using Berenice.Core.Dtos;
using Berenice.Core.Interfaces;
using Berenice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Berenice.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BereniceDBContext bereniceDBContext;
        private readonly ICustomersRepository customersRepository;
        private readonly IProductRepository productRepository;
        public OrderRepository(BereniceDBContext bereniceDBContext, ICustomersRepository customersRepository, 
            IProductRepository productRepository) 
        {
            this.bereniceDBContext = bereniceDBContext;
            this.customersRepository = customersRepository;
            this.productRepository = productRepository;
        }
        public async Task<ApiResponse<OrderDTO>> AddOrder(OrderDTO product)
        {
            //usar AuoMapper
            var orderDb = new Order
            { 
                ProductId = product.ProductId,
                CustomerId = product.CustomerId,
                OrderDate = DateTime.Now,
                RequiredDate = product.RequiredDate,
                ShippedDate = DateTime.Now,
                OrderStatus = 1
            }; 
            await bereniceDBContext.Orders.AddAsync(orderDb);
            int result = await bereniceDBContext.SaveChangesAsync();
            if (result > 0)
                product.ProductId = orderDb.ProductId;

            return new ApiResponse<OrderDTO>
            {
                Response = (result > 0) ? product : null,
                Message = (result > 0) ? "Correcto" : "Error",
                Status = result > 0
            };

        }
        public async Task<ApiResponse<OrderDTO>> GetOrderById(int orderId)
        {
            var order = await bereniceDBContext.Orders.Where(x => x.ProductId == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                var customer = await customersRepository.GetCustomerById(order.CustomerId);
                var product = await productRepository.GetProductById(order.ProductId);
                var productDto = new OrderDTO
                {
                    OrderId = order.OrderId,
                    CustomerId= order.CustomerId,
                    OrderStatus= order.OrderStatus,
                    ProductId = order.ProductId,
                    RequiredDate= order.RequiredDate,
                    ShippedDate = order.ShippedDate,
                    OrderDate = order.OrderDate,
                    Customer = customer.Response,
                    Product = product.Response
                };
                return new ApiResponse<OrderDTO>
                {
                    Response = productDto,
                    Message = "Correcto",
                    Status = true
                };
            }
            else
            {
                return new ApiResponse<OrderDTO>
                {
                    Response = null,
                    Message = "No hay Pedido",
                    Status = false
                };
            }
        }
        public async Task<ApiResponse<OrderDTO>> UpdateOrder(OrderDTO orderDTO)
        {
            var order = await bereniceDBContext.Orders.Where(x => x.OrderId == orderDTO.OrderId)
                .FirstOrDefaultAsync();
            if (order != null)
            {
                order.OrderStatus = orderDTO.OrderStatus;
                order.OrderDate = orderDTO.OrderDate;
                order.RequiredDate = orderDTO.RequiredDate;
                order.ShippedDate = orderDTO.ShippedDate;

                int result = await bereniceDBContext.SaveChangesAsync();
                return new ApiResponse<OrderDTO>
                {
                    Response = orderDTO,
                    Message = (result > 0) ? "Pedido actualizado" : "Error",
                    Status = result > 0
                };
            }
            else
            {
                return new ApiResponse<OrderDTO>
                {
                    Status = false,
                    Message = "No se realizo la actualziación",
                    Response = null,
                };
            }
        }
        public async Task<ApiResponse<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orderResponse = await bereniceDBContext.Orders.ToListAsync();
            if (orderResponse.Count > 0)
            {
                var listDto = new List<OrderDTO>();
                orderResponse.ForEach( async item =>
                {
                    var customer = await customersRepository.GetCustomerById(item.CustomerId);
                    var product = await productRepository.GetProductById(item.ProductId);
                    listDto.Add(new OrderDTO
                    {
                        OrderId = item.OrderId,
                        CustomerId = item.CustomerId,
                        OrderStatus = item.OrderStatus,
                        ProductId = item.ProductId,
                        RequiredDate = item.RequiredDate,
                        ShippedDate = item.ShippedDate,
                        OrderDate = item.OrderDate,
                        Customer = customer.Response,
                        Product = product.Response
                    });
                });
                return new ApiResponse<IEnumerable<OrderDTO>>
                {
                    Response = listDto,
                    Message = string.Empty,
                    Status = true
                };
            }
            else
            {

                return new ApiResponse<IEnumerable<OrderDTO>>
                {
                    Response = null,
                    Message = string.Empty,
                    Status = false
                };
            }
        }
        public async Task<ApiResponse<string>> DeleteOrder(int orderId)
        {

            var order = await bereniceDBContext.Orders.Where(x => x.ProductId == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                bereniceDBContext.Orders.Remove(order);
                int result = await bereniceDBContext.SaveChangesAsync();
                return new ApiResponse<string>
                {
                    Response = string.Empty,
                    Status = result > 0,
                    Message = (result > 0) ? "Pedido Eliminado" : "No fue posible Eliminar el Pedido"
                };
            }
            else
            {

                return new ApiResponse<string>
                {
                    Response = string.Empty,
                    Status = false,
                    Message = "No fue posible Eliminar el Pedido"
                };
            }
        }
    }
}
