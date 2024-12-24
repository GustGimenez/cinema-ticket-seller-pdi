using Application.Models;

namespace Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public string? AuthenticateUser(User user, string password);
    }
}