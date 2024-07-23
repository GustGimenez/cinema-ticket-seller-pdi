using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinema_ticket_seller_pdi.Models
{
    [Table("employees")]
    public class Employee : User
    {
        [Column("movie_theater_id"), Required]
        public long MovieThaterId { get; set; }


        public MovieTheater MovieTheater { get; set; }
    }
}
