using Application.Contexts;
using Application.Models;
using Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class MovieTheaterRepository : IMovieTheaterRepository
    {
        private readonly TicketSellerContext _context;

        public MovieTheaterRepository(TicketSellerContext context)
        {
            _context = context;
        }

        public async Task<MovieTheater> Create(string name)
        {
            var movieTheater = new MovieTheater
            {
                Name = name
            };

            _context.MovieTheaters.Add(movieTheater);
            await _context.SaveChangesAsync();

            return movieTheater;
        }

        public async Task<MovieTheater?> FindByName(string name)
        {
            var movieTheater = await _context.MovieTheaters.FirstOrDefaultAsync(mt => mt.Name == name);

            return movieTheater;
        }

        public async Task<IEnumerable<MovieTheater>> GetAll()
        {
            return await _context.MovieTheaters.ToListAsync();
        }

        public async Task<MovieTheater?> FindById(long id)
        {
            return await _context.MovieTheaters.FindAsync(id);
        }
    }
}