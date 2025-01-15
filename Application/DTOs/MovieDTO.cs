using Application.Models;

namespace Application.DTOs;

public class MovieDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ParentalRating ParentalRating { get; set; }
    public long MovieTheaterId { get; set; }
}