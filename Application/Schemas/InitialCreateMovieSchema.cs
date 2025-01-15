using System.ComponentModel.DataAnnotations;
using Application.Models;

namespace Application.Schemas;

public class InitialCreateMovieSchema
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Classificação indicativa é obrigatória")]
    public ParentalRating ParentalRating { get; set; }
}