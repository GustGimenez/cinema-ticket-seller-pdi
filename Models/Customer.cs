using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema_ticket_seller_pdi.Models
{
    [Table("customers")]
    public class Customer : User
    {
        [Column("document", TypeName = "varchar(11)"), Required]
        public string Document { get; set; }

        [Column("birth_date"), Required]
        public DateTime BirthDate { get; set; }
    }
}
