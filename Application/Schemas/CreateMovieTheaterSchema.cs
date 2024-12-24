using System.ComponentModel.DataAnnotations;

namespace Application.Schemas
{
    public class CreateMovieTheaterSchema
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Nome deve ter entre 5 e 100 caracteres")]
        public string Name { get; set; } = null!;
    }
}