using cinema_ticket_seller_pdi.Models;
using cinema_ticket_seller_pdi.Schemas;

namespace cinema_ticket_seller_pdi.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<User?> FindById(long id);

        public Task<User?> FindByDocument(string document);

        public Task<User> Create(CreateEmployeeSchema createEmployeeSchema);

        public Task Update(long id, UpdateEmployeeSchema updateEmployeeSchema);

        public Task<User> Deactivate(long id);
    }    
}