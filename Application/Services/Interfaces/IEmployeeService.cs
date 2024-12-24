using Application.DTOs;
using Application.Schemas;

namespace Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<EmployeeDTO> Create(CreateEmployeeSchema createEmployeeSchema);

        public Task Update(long id, UpdateEmployeeSchema updateEmployeeSchema);

        public Task<long> Deactivate(long userMovieTheaterId, long deactivateId);
    }
}
