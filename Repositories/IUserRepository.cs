using cinema_ticket_seller_pdi.Models;

namespace cinema_ticket_seller_pdi.Repositories
{
    public interface IUserRepository
    {
        public Task<User> FindByDocument(string document);
    }
}
