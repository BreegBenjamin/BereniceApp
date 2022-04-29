using Berenice.Core.Dtos;

namespace Berenice.Core.Interfaces
{
    public interface ICustomersRepository
    {
        Task<ApiResponse<IEnumerable<CustomerDTO>>> GetCustomers();
        Task<ApiResponse<CustomerDTO>> AddCustomer(CustomerDTO customer);
        Task<ApiResponse<CustomerDTO>> GetCustomerById(int customerId);
        Task<ApiResponse<CustomerDTO>> UpdateCustomer(CustomerDTO customerDto);
        Task<ApiResponse<string>> DeleteCustomer(int customerId);
    }
}
