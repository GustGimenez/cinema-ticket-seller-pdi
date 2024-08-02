using cinema_ticket_seller_pdi.Constants;
using cinema_ticket_seller_pdi.Schemas;
using cinema_ticket_seller_pdi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema_ticket_seller_pdi.Controllers
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

            // TODO: checkout what is the correct return for the first parameter
            return Created("", movieTheater);
        }
    }
}