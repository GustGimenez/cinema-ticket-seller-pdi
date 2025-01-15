using Application.DTOs;
using Application.Schemas;

namespace Application.Services.Interfaces;

public interface IMovieService
{
    Task<MovieDTO> Create(CreateMovieSchema movieSchema);
}