using cinema_ticket_seller_pdi.DTOs;
using cinema_ticket_seller_pdi.Exceptions;
using cinema_ticket_seller_pdi.Repositories;

namespace cinema_ticket_seller_pdi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDTO> FindById(long id)
        {
            var customer = await _customerRepository.FindById(id);

            if (customer == null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }

            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Document = customer.Document,
                BirthDate = customer.BirthDate,
                Active = customer.Active,
            };
        }
    }
}