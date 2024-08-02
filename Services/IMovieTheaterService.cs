using cinema_ticket_seller_pdi.DTOs;

namespace cinema_ticket_seller_pdi.Services
{
    public interface IMovieTheaterService
    {
        public Task<MovieTheaterDTO> Create(string name);

        public Task<IEnumerable<MovieTheaterDTO>> GetAll();
    }
}