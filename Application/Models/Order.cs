using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
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

        [Column("user_id"), Required]
        public long UserId { get; set; }


        public User User { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
