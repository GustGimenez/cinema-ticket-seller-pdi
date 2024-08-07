using cinema_ticket_seller_pdi.Models;

namespace cinema_ticket_seller_pdi.Repositories
{
    public interface ICustomerRepository
    {
        public Task<User?> FindById(long id);
    }
}