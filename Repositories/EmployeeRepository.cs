using cinema_ticket_seller_pdi.Contexts;
using cinema_ticket_seller_pdi.Models;
using cinema_ticket_seller_pdi.Schemas;
using Microsoft.EntityFrameworkCore;

namespace cinema_ticket_seller_pdi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TicketSellerContext _context;

        public EmployeeRepository(TicketSellerContext context)
        {
            _context = context;
        }

        public async Task<User?> FindById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(
                employee => employee.Role == Role.Employee && employee.Id == id
            );
        }

        public async Task<User?> FindByDocument(string document)
        {
            return await _context.Users.FirstOrDefaultAsync(
                employee => employee.Role == Role.Employee && employee.Document == document
            );
        }

        public async Task<User> Create(CreateEmployeeSchema createEmployeeSchema)
        {
            var employee = new User()
            {
                Name = createEmployeeSchema.Name,
                Document = createEmployeeSchema.Document,
                Role = Role.Employee,
                MovieTheaterId = createEmployeeSchema.MovieTheaterId,
                BirthDate = createEmployeeSchema.BirthDate,
            };

            _context.Users.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
    }
}