using Application.Constants;
using Application.Schemas;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/movie-theater")]
    [ApiController]
    public class MovieTheaterController : ControllerBase
    {
        private readonly IMovieTheaterService _movieTheaterService;

        public MovieTheaterController(IMovieTheaterService movieTheaterService)
        {
            _movieTheaterService = movieTheaterService;
        }

        [HttpPost]
        [Authorize(Roles = AuthorizationRolesConstants.Administrator)]
        public async Task<IActionResult> Create([FromBody] CreateMovieTheaterSchema movieTheaterDTO)
        {
            var movieTheater = await _movieTheaterService.Create(movieTheaterDTO.Name);

            return Created("", movieTheater);
        }

        [HttpGet]
        [Authorize(Roles = AuthorizationRolesConstants.Administrator)]
        public async Task<IActionResult> GetAll()
        {
            var movieTheaters = await _movieTheaterService.GetAll();

            return Ok(movieTheaters);
        }
    }
}