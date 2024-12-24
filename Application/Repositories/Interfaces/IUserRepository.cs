using Application.Models;

namespace Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> FindByDocument(string document);
    }
}
