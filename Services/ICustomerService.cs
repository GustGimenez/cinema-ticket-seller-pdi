using cinema_ticket_seller_pdi.DTOs;
using cinema_ticket_seller_pdi.Schemas;

namespace cinema_ticket_seller_pdi.Services
{
    public interface ICustomerService
    {
        public Task<CustomerDTO> FindById(long id);

        public Task<CustomerDTO> Create(CreateCustomerSchema schema);

        public Task<CustomerDTO> Update(long id, UpdateCustomerSchema schema);

        public Task Deactivate(long id);
    }
}
