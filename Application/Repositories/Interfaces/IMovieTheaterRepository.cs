using Application.Models;

namespace Application.Repositories.Interfaces
{
    public interface IMovieTheaterRepository
    {
        public Task<MovieTheater> Create(string name);

        public Task<MovieTheater?> FindByName(string name);

        public Task<IEnumerable<MovieTheater>> GetAll();

        public Task<MovieTheater?> FindById(long id);
    }
}