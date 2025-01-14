using Application.Contexts;
using Application.Models;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Application.Repositories;

public class UserRepositoryTests
{
    private readonly TicketSellerContext _context;
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TicketSellerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TicketSellerContext(options);
        _repository = new UserRepository(_context);
    }

    [Fact]
    public async Task FindByDocument_ShouldReturnUser_WhenUserExists()
    {
        var user = new User
        {
            Id = 1,
            Name = "John Doe",
            Password = "password123",
            Document = "123456789",
            BirthDate = DateTime.Now,
            Role = Role.Customer,
            Active = true,
            MovieTheaterId = null
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _repository.FindByDocument("123456789");

        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        Assert.Equal("123456789", result.Document);
    }

    [Fact]
    public async Task FindByDocument_ShouldReturnNull_WhenUserDoesNotExist()
    {
        var result = await _repository.FindByDocument("987654321");

        Assert.Null(result);
    }
}