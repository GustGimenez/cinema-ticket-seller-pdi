using cinema_ticket_seller_pdi.Constants;
using cinema_ticket_seller_pdi.Exceptions;
using cinema_ticket_seller_pdi.Schemas;
using cinema_ticket_seller_pdi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema_ticket_seller_pdi.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Authorize(Roles = AuthorizationRolesConstants.Employee)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeSchema createEmployeeSchema)
        {
            var userMovieTheaterId = User.Claims.FirstOrDefault(c => c.Type == "movie_theater_id")?.Value;
            var movieTheaterId = userMovieTheaterId != null ? int.Parse(userMovieTheaterId) : createEmployeeSchema.MovieTheaterId;

            if (movieTheaterId == null)
            {
                throw new LogicValidationException("O funcionário deve pertencer a um cinema");
            }

            createEmployeeSchema.MovieTheaterId = movieTheaterId;

            var employee = await _employeeService.Create(createEmployeeSchema);

            return Created("", employee);
        }
    }
}