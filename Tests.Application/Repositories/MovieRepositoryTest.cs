using Application.Contexts;
using Application.Models;
using Application.Repositories;
using Application.Repositories.Interfaces;
using Application.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Tests.Application.Repositories;

public class MovieRepositoryTest
{
    private readonly TicketSellerContext _context;
    private readonly IMovieRepository _movieRepository;

    public MovieRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<TicketSellerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TicketSellerContext(options);
        _movieRepository = new MovieRepository(_context);
    }

    [Fact]
    public async Task Create_ShouldReturnMovie_WhenMovieIsCreated()
    {
        var createMovieSchema = new CreateMovieSchema
        {
            Name = "Movie Name",
            ParentalRating = ParentalRating.A10,
            MovieTheaterId = 1
        };
        var movie = new Movie
        {
            Name = createMovieSchema.Name,
            ParentalRating = createMovieSchema.ParentalRating,
            MovieTheaterId = createMovieSchema.MovieTheaterId
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        var result = await _movieRepository.Create(createMovieSchema);

        Assert.NotNull(result);
        Assert.Equal(createMovieSchema.Name, result.Name);
    }

    [Fact]
    public async Task FindByName_ShouldReturnMovie_WhenMovieExists()
    {
        var movie = new Movie
        {
            Name = "Movie Name",
            ParentalRating = ParentalRating.A10,
            MovieTheaterId = 1
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        var result = await _movieRepository.FindByName(movie.Name);

        Assert.NotNull(result);
        Assert.Equal(movie.Name, result.Name);
    }

    [Fact]
    public async Task FindByName_ShouldReturnNull_WhenMovieDoesNotExist()
    {
        _context.Movies.RemoveRange(_context.Movies);
        await _context.SaveChangesAsync();

        var result = await _movieRepository.FindByName("Movie Name");

        Assert.Null(result);
    }
}