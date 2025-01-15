using Application.Models;
using Application.Schemas;

namespace Application.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<Movie> Create(CreateMovieSchema movieSchema);
    Task<Movie?> FindByName(string name);
}