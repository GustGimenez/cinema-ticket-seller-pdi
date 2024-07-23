using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema_ticket_seller_pdi.Models
{
    public class User
    {
        [Column("id"), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("name", TypeName = "varchar(100)"), Required]
        public string Name { get; set; }

        [Column("password", TypeName = "varchar(20)"), Required]
        public string Password { get; set; }

        [Column("active"), Required]
        public Boolean Active { get; set; }
    }
}