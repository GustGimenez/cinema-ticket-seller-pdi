using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema_ticket_seller_pdi.Models
{
    [Table("movie_sessions")]
    public class MovieSession
    {
        [Column("id"), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("from"), Required]
        public DateTime From { get; set; }

        [Column("to"), Required]
        public DateTime To { get; set; }

        [Column("seats"), Required]
        public int Seats { get; set; }

        [Column("active"), Required]
        public Boolean Active { get; set; }

        [Column("price"), Required]
        public decimal Price { get; set; }

        [Column("movie_id"), Required]
        public long MovieId { get; set; }


        public Movie Movie { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
