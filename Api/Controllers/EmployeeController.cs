using Application.Constants;
using Application.Schemas;
using Application.Services.Interfaces;
using Commons.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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
                throw new LogicException("O funcionário deve pertencer a um cinema");
            }

            createEmployeeSchema.MovieTheaterId = movieTheaterId;

            var employee = await _employeeService.Create(createEmployeeSchema);

            return Created("", employee);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = AuthorizationRolesConstants.Employee)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateEmployeeSchema updateEmployeeSchema)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (userId == null || int.Parse(userId) != id)
            {
                throw new LogicException("Funcionário só pode editar a si mesmo");
            }

            await _employeeService.Update(id, updateEmployeeSchema);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = AuthorizationRolesConstants.Employee)]
        public async Task<IActionResult> Deactivate(long id)
        {
            var userMovieTheaterId = User.Claims.FirstOrDefault(c => c.Type == "movie_theater_id")?.Value;
            var movieTheaterId = long.Parse(userMovieTheaterId);

            await _employeeService.Deactivate(movieTheaterId, id);

            return Ok();
        }
    }
}