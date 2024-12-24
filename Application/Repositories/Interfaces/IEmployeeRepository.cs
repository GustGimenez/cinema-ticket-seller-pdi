using Application.Models;
using Application.Schemas;

namespace Application.Repositories.Interfaces
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