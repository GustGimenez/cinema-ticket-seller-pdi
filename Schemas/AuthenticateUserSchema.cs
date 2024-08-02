using System.ComponentModel.DataAnnotations;

namespace cinema_ticket_seller_pdi.Schemas
{
    public class AuthenticateUserSchema
    {
        [Required(ErrorMessage = "Documento é obrigatório")]
        [StringLength(11, ErrorMessage = "Documento deve ter 11 caracteres")]
        public string Document { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 10 caracteres")]
        public string Password { get; set; }
    }
}
