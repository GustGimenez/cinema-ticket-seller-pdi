using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema_ticket_seller_pdi.Models
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Canceled
    }

    [Table("orders")]
    public class Order
    {
        [Column("id"), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("total"), Required]
        public decimal Total { get; set; }

        [Column("ticket_quantity"), Required]
        public int TicketQuantity { get; set; }

        [Column("status"), Required]
        public OrderStatus Status { get; set; }

        [Column("created_at"), Required]
        public DateTime CreatedAt { get; set; }

        [Column("movie_session_id"), Required]
        public long CustomerId { get; set; }


        public Customer Customer { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
