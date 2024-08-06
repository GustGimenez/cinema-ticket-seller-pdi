using cinema_ticket_seller_pdi.DTOs;
using cinema_ticket_seller_pdi.Schemas;

namespace cinema_ticket_seller_pdi.Services
{
    public interface IEmployeeService
    {
        public Task<EmployeeDTO> Create(CreateEmployeeSchema createEmployeeSchema);
    }
}