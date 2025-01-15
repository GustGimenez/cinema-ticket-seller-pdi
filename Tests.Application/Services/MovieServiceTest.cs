using Application.Models;
using Application.Repositories.Interfaces;
using Application.Schemas;
using Application.Services;
using Application.Services.Interfaces;
using Commons.Exceptions;
using Moq;

namespace Tests.Application.Services;

public class MovieServiceTest
{
    private readonly Mock<IMovieRepository> _movieRepositoryMock;
    private readonly IMovieService _movieService;
    private const string MovieName = "Movie Name";

    public MovieServiceTest()
    {
        _movieRepositoryMock = new Mock<IMovieRepository>();
        _movieService = new MovieService(_movieRepositoryMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnAMovieDTO_WhenMovieIsCreated()
    {
        var movieSchema = new CreateMovieSchema
        {
            Name = MovieName,
            ParentalRating = ParentalRating.A18,
            MovieTheaterId = 1
        };
        var createdMovie = new Movie
        {
            Id = 1,
            Name = movieSchema.Name,
            ParentalRating = movieSchema.ParentalRating,
            MovieTheaterId = movieSchema.MovieTheaterId
        };
        _movieRepositoryMock.Setup(x => x.Create(movieSchema)).ReturnsAsync(createdMovie);

        var result = await _movieService.Create(movieSchema);

        Assert.NotNull(result);
        Assert.Equal(MovieName, result.Name);
        Assert.Equal(movieSchema.ParentalRating, result.ParentalRating);
        Assert.Equal(movieSchema.MovieTheaterId, result.MovieTheaterId);
    }

    [Fact]
    public async Task Create_ShouldThrowALogicException_WhenMovieAlreadyExists()
    {
        var movieSchema = new CreateMovieSchema
        {
            Name = MovieName,
            ParentalRating = ParentalRating.A18,
            MovieTheaterId = 1
        };
        var existingMovie = new Movie
        {
            Id = 1,
            Name = movieSchema.Name,
            ParentalRating = movieSchema.ParentalRating,
            MovieTheaterId = movieSchema.MovieTheaterId
        };
        _movieRepositoryMock.Setup(x => x.FindByName(MovieName)).ReturnsAsync(existingMovie);

        var exception = await Assert.ThrowsAsync<LogicException>(() => _movieService.Create(movieSchema));

        Assert.Equal("Filme já cadastrado", exception.Message);
        _movieRepositoryMock.Verify(x => x.FindByName(MovieName), Times.Once);
    }
}