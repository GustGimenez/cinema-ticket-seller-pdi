using cinema_ticket_seller_pdi.Contexts;
using cinema_ticket_seller_pdi.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema_ticket_seller_pdi.Repositories
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
            var movieTheater = await _context.MovieTheaters.FirstOrDefaultAsync((mt) => mt.Name == name);

            return movieTheater;
        }
    }
}