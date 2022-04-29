using Berenice.Core.Data;
using Berenice.Core.Dtos;
using Berenice.Core.Interfaces;
using Berenice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Berenice.Infrastructure.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly BereniceDBContext bereniceDBContext;
        public CustomersRepository(BereniceDBContext bereniceDBContext) 
        {
            this.bereniceDBContext = bereniceDBContext;
        }

        public async Task<ApiResponse<CustomerDTO>> AddCustomer(CustomerDTO customer)
        {
            //usar AuoMapper
            var customerDb = new Customer 
            {
                City = customer.City,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                Street = customer.Street,
                ZipCode = customer.ZipCode,
                State = "Active",
            };
            await bereniceDBContext.Customers.AddAsync(customerDb);
            int result = await bereniceDBContext.SaveChangesAsync();
            if (result > 0)
                customer.CustomerId = customerDb.CustomerId;

            return new ApiResponse<CustomerDTO>
            {
                Response = (result > 0) ? customer : null,
                Message = (result > 0) ? "Correcto" : "Error",
                Status = result > 0
            };
            
        }

        public async Task<ApiResponse<CustomerDTO>> GetCustomerById(int customerId)
        {
            var customer = await bereniceDBContext.Customers.Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
            if (customer != null)
            {
                var customerDto = new CustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    City = customer.City,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Phone = customer.Phone,
                    Street = customer.Street,
                    ZipCode = customer.ZipCode,
                    State = customer.State,
                    OrdersCount = bereniceDBContext.Orders.Count(x => x.CustomerId == customerId)
                };
                return new ApiResponse<CustomerDTO>
                {
                    Response = customerDto,
                    Message = "Correcto",
                    Status = true
                };
            }
            else 
            {
                return new ApiResponse<CustomerDTO>
                {
                    Response = null,
                    Message = "No hay cliente",
                    Status = false
                };
            }
        }
        public async Task<ApiResponse<CustomerDTO>> UpdateCustomer(CustomerDTO customerDto) 
        {
            var customer = await bereniceDBContext.Customers.Where(x => x.CustomerId == customerDto.CustomerId)
                .FirstOrDefaultAsync();
            if (customer != null)
            {
                customer.Street = customerDto.Street;
                customer.ZipCode = customerDto.ZipCode;
                customer.State = customerDto.State; 
                customer.City = customerDto.City;
                customer.Email = customerDto.Email;
                customer.Phone = customerDto.Phone;
                customer.FirstName = customerDto.FirstName;
                customer.LastName = customerDto.LastName;

                int result = await bereniceDBContext.SaveChangesAsync();
                return new ApiResponse<CustomerDTO>
                {
                    Response = customerDto,
                    Message = (result > 0) ? "Cliente actualizado" : "Error",
                    Status = result > 0
                };
            }
            else 
            {
                return new ApiResponse<CustomerDTO>
                {
                    Status = false,
                    Message = "No se realizo la actualziación",
                    Response = null,
                };
            }
        }
        public async Task<ApiResponse<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var response = await bereniceDBContext.Customers.ToListAsync();
            if (response.Count > 0)
            {
                var listDto = new List<CustomerDTO>();
                response.ForEach(item=> 
                {
                    listDto.Add(new CustomerDTO 
                    {
                        City = item.City,
                        CustomerId = item.CustomerId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email  = item.Email,
                        Phone = item.Phone,
                        State = item.State,
                        Street = item.Street,
                        ZipCode = item.ZipCode,
                        OrdersCount = bereniceDBContext.Orders.Count(x=> x.CustomerId == item.CustomerId)
                    });
                });
                return new ApiResponse<IEnumerable<CustomerDTO>>
                {
                    Response = listDto,
                    Message = string.Empty,
                    Status = true
                };
            }
            else 
            {

                return new ApiResponse<IEnumerable<CustomerDTO>>
                {
                    Response = null,
                    Message = string.Empty,
                    Status = false
                };
            }
        }
        public async Task<ApiResponse<string>> DeleteCustomer(int customerId) 
        {

            var customer = await bereniceDBContext.Customers.Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
            if (customer != null)
            {
                bereniceDBContext.Customers.Remove(customer);
                int result = await bereniceDBContext.SaveChangesAsync();
                return new ApiResponse<string>
                {
                    Response =  string.Empty,
                    Status = result > 0,
                    Message = (result > 0) ?  "Cliente Eliminado" : "No fue posible Eliminar al cliente"
                };
            }
            else 
            {

                return new ApiResponse<string>
                {
                    Response = string.Empty,
                    Status = false,
                    Message = "No fue posible Eliminar al cliente"
                };
            }
        }
    }
}
