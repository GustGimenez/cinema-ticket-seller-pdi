using Application.Constants;
using Application.DTOs;
using Application.Schemas;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/movie")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost]
    [Authorize(Roles = AuthorizationRolesConstants.Employee)]
    public async Task<ActionResult<MovieDTO>> Create([FromBody] InitialCreateMovieSchema initialCreateMovieSchema)
    {
        var userMovieTheaterId = User.Claims.FirstOrDefault(c => c.Type == "movie_theater_id")?.Value;
        var createMovieSchema = new CreateMovieSchema
        {
            Name = initialCreateMovieSchema.Name,
            ParentalRating = initialCreateMovieSchema.ParentalRating,
            MovieTheaterId = long.Parse(userMovieTheaterId)
        };
        var createdMovie = await _movieService.Create(createMovieSchema);

        return Created("", createdMovie);
    }
}