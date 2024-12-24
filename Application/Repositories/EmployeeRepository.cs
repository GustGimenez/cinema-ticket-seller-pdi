using Application.Contexts;
using Application.Models;
using Application.Repositories.Interfaces;
using Application.Schemas;
using Commons.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TicketSellerContext _context;

        public EmployeeRepository(TicketSellerContext context)
        {
            _context = context;
        }

        public async Task<User?> FindById(long id)
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
                Password = createEmployeeSchema.Password,
                Active = true,
            };

            _context.Users.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task Update(long id, UpdateEmployeeSchema updateEmployeeSchema)
        {
            var employee = await FindById(id);

            if (employee == null)
            {
                throw new DataNotFoundException("Funcionário não encontrado");
            }

            employee.Name = updateEmployeeSchema.Name;
            employee.BirthDate = updateEmployeeSchema.BirthDate;
            employee.Password = updateEmployeeSchema.Password;

            await _context.SaveChangesAsync();
        }

        public async Task<User> Deactivate(long id)
        {
            var employee = await FindById(id);

            if (employee == null)
            {
                throw new DataNotFoundException("Funcionário não encontrado");
            }

            employee.Active = false;
            await _context.SaveChangesAsync();

            return employee;
        }
    }
}