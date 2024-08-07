using cinema_ticket_seller_pdi.Contexts;
using cinema_ticket_seller_pdi.Exceptions;
using cinema_ticket_seller_pdi.Models;
using cinema_ticket_seller_pdi.Schemas;
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

        public async Task<User?> FindByDocument(string document)
        {
            return await _context.Users.FirstOrDefaultAsync(
                customer => customer.Document == document && customer.Role == Role.Customer
            );
        }

        public async Task<User> Create(CreateCustomerSchema schema)
        {
            var customer = new User
            {
                Name = schema.Name,
                Document = schema.Document,
                BirthDate = schema.BirthDate,
                Role = Role.Customer,
                Active = true,
                Password = schema.Password,
            };

            _context.Users.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<User> Update(long id, UpdateCustomerSchema schema)
        {
            var customer = await FindById(id);

            if (customer == null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }

            customer.Name = schema.Name;
            customer.BirthDate = schema.BirthDate;
            customer.Password = schema.Password;

            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
