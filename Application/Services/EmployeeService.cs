using Application.DTOs;
using Application.Repositories.Interfaces;
using Application.Schemas;
using Application.Services.Interfaces;
using Commons.Exceptions;

namespace Application.Services
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

        public async Task Update(long id, UpdateEmployeeSchema updateEmployeeSchema)
        {
            await _employeeRepository.Update(id, updateEmployeeSchema);
        }

        public async Task<long> Deactivate(long userMovieTheaterId, long deactivateId)
        {
            var employee = await _employeeRepository.FindById(deactivateId);

            if (employee == null)
            {
                throw new DataNotFoundException("Funcionário não encontrado");
            }

            if (employee.MovieTheaterId != userMovieTheaterId)
            {
                throw new LogicException("Funcionário pertence à outra agência de cinema");
            }

            var deletedUser = await _employeeRepository.Deactivate(deactivateId);

            return deletedUser.Id;
        }

        private async Task ValidateCreation(CreateEmployeeSchema createEmployeeSchema)
        {
            var exists = await _employeeRepository.FindByDocument(createEmployeeSchema.Document);

            if (exists != null)
            {
                throw new LogicException("Funcionário com esse CPF já existente");
            }

            var movieTheater = await _movieTheaterRepository.FindById(createEmployeeSchema.MovieTheaterId.Value);

            if (movieTheater == null)
            {
                throw new LogicException("Cinema não encontrado");
            }
        }
    }
}