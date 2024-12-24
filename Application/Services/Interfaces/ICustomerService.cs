using Application.DTOs;
using Application.Schemas;

namespace Application.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomerDTO> FindById(long id);

        public Task<CustomerDTO> Create(CreateCustomerSchema schema);

        public Task<CustomerDTO> Update(long id, UpdateCustomerSchema schema);

        public Task Deactivate(long id);
    }
}
