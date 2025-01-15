using Application.Contexts;
using Application.Models;
using Application.Repositories.Interfaces;
using Application.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly TicketSellerContext _context;

    public MovieRepository(TicketSellerContext context)
    {
        _context = context;
    }

    public async Task<Movie> Create(CreateMovieSchema movieSchema)
    {
        var movie = new Movie
        {
            Name = movieSchema.Name,
            ParentalRating = movieSchema.ParentalRating,
            MovieTheaterId = movieSchema.MovieTheaterId
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<Movie?> FindByName(string name)
    {
        return await _context.Movies.FirstOrDefaultAsync(x => x.Name == name);
    }
}