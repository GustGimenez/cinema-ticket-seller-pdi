using cinema_ticket_seller_pdi.Models;

namespace cinema_ticket_seller_pdi.Repositories
{
    public interface IMovieTheaterRepository
    {
        public Task<MovieTheater> Create(string name);

        public Task<MovieTheater?> FindByName(string name);
    }
}