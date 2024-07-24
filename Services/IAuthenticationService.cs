using cinema_ticket_seller_pdi.Models;

namespace cinema_ticket_seller_pdi.Services
{
    public interface IAuthenticationService
    {
        public string? AuthenticateUser(User user, string password);
    }
}
