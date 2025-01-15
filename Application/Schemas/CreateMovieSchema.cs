using System.ComponentModel.DataAnnotations;

namespace Application.Schemas;

public class CreateMovieSchema : InitialCreateMovieSchema
{
    [Required(ErrorMessage = "Id do cinema é obrigatório")]
    public long MovieTheaterId { get; set; }
}