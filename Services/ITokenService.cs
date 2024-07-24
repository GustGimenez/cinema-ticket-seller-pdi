using cinema_ticket_seller_pdi.Models;

namespace cinema_ticket_seller_pdi.Services
{
    public interface ITokenService
    {
        string GenerateJWTForUser(User user);
    }
}
