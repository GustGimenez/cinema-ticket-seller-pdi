using cinema_ticket_seller_pdi.Models;
using cinema_ticket_seller_pdi.Schemas;

namespace cinema_ticket_seller_pdi.Repositories
{
    public interface ICustomerRepository
    {
        public Task<User?> FindById(long id);

        public Task<User?> FindByDocument(string document);

        public Task<User> Create(CreateCustomerSchema customer);

        public Task<User> Update(long id, UpdateCustomerSchema customer);
    }
}
