using cinema_ticket_seller_pdi.DTOs;
using cinema_ticket_seller_pdi.Exceptions;
using cinema_ticket_seller_pdi.Repositories;
using cinema_ticket_seller_pdi.Schemas;

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

        public async Task<CustomerDTO> Create(CreateCustomerSchema customer)
        {
            var existingCustomer = await _customerRepository.FindByDocument(customer.Document);

            if (existingCustomer != null)
            {
                throw new LogicValidationException("Cliente já cadastrado");
            }

            var cratedCustomer = await _customerRepository.Create(customer);

            return new CustomerDTO
            {
                Id = cratedCustomer.Id,
                Name = cratedCustomer.Name,
                Document = cratedCustomer.Document,
                BirthDate = cratedCustomer.BirthDate,
                Active = cratedCustomer.Active,
            };
        }

        public async Task<CustomerDTO> Update(long id, UpdateCustomerSchema customerData)
        {
            var customer = await _customerRepository.FindById(id);

            if (customer == null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }

            await _customerRepository.Update(id, customerData);

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
