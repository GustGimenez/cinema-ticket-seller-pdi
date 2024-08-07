using cinema_ticket_seller_pdi.Contexts;
using cinema_ticket_seller_pdi.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema_ticket_seller_pdi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TicketSellerContext _context;

        public CustomerRepository(TicketSellerContext context)
        {
            _context = context;
        }

        public async Task<User?> FindById(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(
                customer => customer.Id == id && customer.Role == Role.Customer
            );
        }
    }
}