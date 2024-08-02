using cinema_ticket_seller_pdi.DTOs;
using cinema_ticket_seller_pdi.Exceptions;
using cinema_ticket_seller_pdi.Repositories;

namespace cinema_ticket_seller_pdi.Services
{
    public class MovieTheaterService : IMovieTheaterService
    {
        private readonly IMovieTheaterRepository _movieTheaterRepository;

        public MovieTheaterService(IMovieTheaterRepository movieTheaterRepository)
        {
            _movieTheaterRepository = movieTheaterRepository;
        }

        public async Task<MovieTheaterDTO> Create(string name)
        {
            var movieTheaterExists = await MovieTheaterExists(name);

            if (movieTheaterExists)
            {
                throw new LogicValidationException("Cinema já cadastrado");
            }

            var movieTheater = await _movieTheaterRepository.Create(name);

            return new MovieTheaterDTO
            {
                Id = movieTheater.Id,
                Name = movieTheater.Name
            };
        }

        public async Task<IEnumerable<MovieTheaterDTO>> GetAll()
        {
            var movieTheaters = await _movieTheaterRepository.GetAll();

            return movieTheaters.Select(mt => new MovieTheaterDTO
            {
                Id = mt.Id,
                Name = mt.Name
            });
        }

        private async Task<bool> MovieTheaterExists(string name)
        {
            var movieTheater = await _movieTheaterRepository.FindByName(name);

            return movieTheater != null;
        }
    }
}