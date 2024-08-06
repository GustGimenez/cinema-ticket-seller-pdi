using cinema_ticket_seller_pdi.DTOs;
using cinema_ticket_seller_pdi.Exceptions;
using cinema_ticket_seller_pdi.Repositories;
using cinema_ticket_seller_pdi.Schemas;

namespace cinema_ticket_seller_pdi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMovieTheaterRepository _movieTheaterRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IMovieTheaterRepository movieTheaterRepository)
        {
            _employeeRepository = employeeRepository;
            _movieTheaterRepository = movieTheaterRepository;
        }

        public async Task<EmployeeDTO> Create(CreateEmployeeSchema createEmployeeSchema)
        {
            await ValidateCreation(createEmployeeSchema);
            var employee = await _employeeRepository.Create(createEmployeeSchema);

            return new EmployeeDTO(){
                Active = employee.Active,
                BirthDate = employee.BirthDate,
                Document = employee.Document,
                Id = employee.Id,
                MovieTheaterId = employee.MovieTheaterId.Value,
                Name = employee.Name
            };
        }

        private async Task ValidateCreation(CreateEmployeeSchema createEmployeeSchema)
        {
            var exists = await _employeeRepository.FindByDocument(createEmployeeSchema.Document);

            if (exists != null)
            {
                throw new LogicValidationException("Funcionário com esse CPF já existente");
            }

            var movieTheater = await _movieTheaterRepository.FindById(createEmployeeSchema.MovieTheaterId.Value);

            if (movieTheater == null)
            {
                throw new LogicValidationException("Cinema não encontrado");
            }
        }
    }
}