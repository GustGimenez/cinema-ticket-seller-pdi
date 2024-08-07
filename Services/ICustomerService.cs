using cinema_ticket_seller_pdi.DTOs;

namespace cinema_ticket_seller_pdi.Services
{
    public interface ICustomerService
    {
        public Task<CustomerDTO> FindById(long id);
    }
}