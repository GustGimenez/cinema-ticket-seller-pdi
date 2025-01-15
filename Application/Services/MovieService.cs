using Application.DTOs;
using Application.Repositories.Interfaces;
using Application.Schemas;
using Application.Services.Interfaces;
using Commons.Exceptions;

namespace Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieDTO> Create(CreateMovieSchema movieSchema)
    {
        var movieExists = await MovieExists(movieSchema.Name);

        if (movieExists)
        {
            throw new LogicException("Filme já cadastrado");
        }

        var createdMovie = await _movieRepository.Create(movieSchema);

        return new MovieDTO
        {
            Id = createdMovie.Id,
            Name = createdMovie.Name,
            ParentalRating = createdMovie.ParentalRating,
            MovieTheaterId = createdMovie.MovieTheaterId
        };
    }

    private async Task<bool> MovieExists(string name)
    {
        var movie = await _movieRepository.FindByName(name);

        return movie != null;
    }
}