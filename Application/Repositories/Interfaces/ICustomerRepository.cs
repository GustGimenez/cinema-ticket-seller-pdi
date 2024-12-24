using Application.Models;
using Application.Schemas;

namespace Application.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<User?> FindById(long id);

        public Task<User?> FindByDocument(string document);

        public Task<User> Create(CreateCustomerSchema customer);

        public Task<User> Update(long id, UpdateCustomerSchema customer);

        public Task Deactivate(long id);
    }
}
