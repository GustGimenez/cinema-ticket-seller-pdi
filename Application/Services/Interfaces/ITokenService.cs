using Application.Models;

namespace Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtForUser(User user);
    }
}
