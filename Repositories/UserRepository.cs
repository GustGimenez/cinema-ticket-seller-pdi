using cinema_ticket_seller_pdi.Contexts;
using cinema_ticket_seller_pdi.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema_ticket_seller_pdi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicketSellerContext _context;

        public UserRepository(TicketSellerContext context)
        {
            _context = context;
        }

        public async Task<User?> FindByDocument(string document)
        {
            var user = await _context.Users.FirstOrDefaultAsync((u) => u.Document == document);

            return user;
        }
    }
}
