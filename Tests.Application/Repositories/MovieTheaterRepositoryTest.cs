using Application.Contexts;
using Application.Models;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;

namespace Tests.Application.Repositories;

public class MovieTheaterRepositoryTest
{
    private const string MovieTheaterName =  "MovieTheater_1";
    private readonly TicketSellerContext _context;
    private readonly MovieTheaterRepository _movieTheaterRepository;

    public MovieTheaterRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<TicketSellerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new TicketSellerContext(options);
        _movieTheaterRepository = new MovieTheaterRepository(_context);
    }
    
    [Fact]
    public async Task Create_ShouldReturnMovieTheater_WhenMovieTheaterIsCreated()
    {
        var createdMovieTheater = await _movieTheaterRepository.Create(MovieTheaterName);
        
        Assert.NotNull(createdMovieTheater);
        Assert.Equal(MovieTheaterName, createdMovieTheater.Name);
    }

    [Fact]
    public async Task FindByName_ShouldReturnMovieTheater_WhenMovieTheaterExists()
    {
        var movieTheater = new MovieTheater
        {
            Name = MovieTheaterName
        };

        _context.MovieTheaters.Add(movieTheater);
        await _context.SaveChangesAsync();
        
        var result = await _movieTheaterRepository.FindByName(MovieTheaterName);

        Assert.NotNull(result);
        Assert.Equal(MovieTheaterName, result.Name);
    }
    
    [Fact]
    public async Task FindByName_ShouldReturnNull_WhenMovieTheaterDoesNotExist()
    {
        await ClearDatabase();
        
        var result = await _movieTheaterRepository.FindByName(MovieTheaterName);

        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetAll_ShouldReturnAllMovieTheaters()
    {
        var movieTheater1 = new MovieTheater
        {
            Name = "MovieTheater_1"
        };
        var movieTheater2 = new MovieTheater
        {
            Name = "MovieTheater_2"
        };
        var movieTheater3 = new MovieTheater
        {
            Name = "MovieTheater_3"
        };

        await ClearDatabase();
        _context.MovieTheaters.AddRange(movieTheater1, movieTheater2, movieTheater3);
        await _context.SaveChangesAsync();
        
        var result = await _movieTheaterRepository.GetAll();
        
        Assert.Equal(3, result.Count());
    }
    
    [Fact]
    public async Task FindById_ShouldReturnMovieTheater_WhenMovieTheaterExists()
    {
        var movieTheater = new MovieTheater
        {
            Name = MovieTheaterName
        };

        _context.MovieTheaters.Add(movieTheater);
        await _context.SaveChangesAsync();
        
        var result = await _movieTheaterRepository.FindById(movieTheater.Id);

        Assert.NotNull(result);
        Assert.Equal(MovieTheaterName, result.Name);
    }
    
    [Fact]
    public async Task FindById_ShouldReturnNull_WhenMovieTheaterDoesNotExist()
    {
        await ClearDatabase();
        
        var result = await _movieTheaterRepository.FindById(1);

        Assert.Null(result);
    }

    private async Task ClearDatabase()
    {
        _context.MovieTheaters.RemoveRange(_context.MovieTheaters);
        await _context.SaveChangesAsync();
    }
}