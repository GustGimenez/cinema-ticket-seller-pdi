using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface IMovieTheaterService
    {
        public Task<MovieTheaterDTO> Create(string name);

        public Task<IEnumerable<MovieTheaterDTO>> GetAll();
    }
}