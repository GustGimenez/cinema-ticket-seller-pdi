using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema_ticket_seller_pdi.Models
{
    [Table("tickets")]
    public class Ticket
    {
        [Column("id"), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("order_id"), Required]
        public long OrderId { get; set; }

        [Column("movie_session_id"), Required]
        public long MovieSessionId { get; set; }


        public Order Order { get; set; }
        public MovieSession MovieSession { get; set; }
    }
}
