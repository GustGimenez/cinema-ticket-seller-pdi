using Application.DTOs;
using Application.Repositories.Interfaces;
using Application.Schemas;
using Application.Services.Interfaces;
using Commons.Exceptions;

namespace Application.Services
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
                throw new DataNotFoundException("Cliente não encontrado");
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
                throw new LogicException("Cliente já cadastrado");
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

        public async Task<CustomerDTO> Update(long id, UpdateCustomerSchema schema)
        {
            var customer = await _customerRepository.FindById(id);

            if (customer == null)
            {
                throw new DataNotFoundException("Cliente não encontrado");
            }

            await _customerRepository.Update(id, schema);

            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Document = customer.Document,
                BirthDate = customer.BirthDate,
                Active = customer.Active,
            };
        }

        public async Task Deactivate(long id)
        {
            var customer = await _customerRepository.FindById(id);

            if (customer == null)
            {
                throw new DataNotFoundException("Cliente não encontrado");
            }

            await _customerRepository.Deactivate(id);
        }
    }
}
