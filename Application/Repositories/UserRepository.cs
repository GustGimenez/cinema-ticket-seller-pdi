using Application.Contexts;
using Application.Models;
using Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
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
            return await _context.Users.FirstOrDefaultAsync(u => u.Document == document);
        }
    }
}
