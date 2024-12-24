using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public enum Role
    {
        Customer,
        Employee,
        Administrator,
    }

    [Table("users")]
    public class User
    {
        [Column("id"), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("name", TypeName = "varchar(100)"), Required]
        public string Name { get; set; }

        [Column("password", TypeName = "varchar(20)"), Required]
        public string Password { get; set; }

        [Column("document", TypeName = "varchar(11)"), Required]
        public string Document { get; set; }

        [Column("birth_date"), Required]
        public DateTime BirthDate { get; set; }

        [Column("role"), Required]
        public Role Role { get; set; }

        [Column("active"), Required]
        public bool Active { get; set; }

        [Column("movie_theater_id")]
        public long? MovieTheaterId { get; set; }


        public MovieTheater? MovieTheater { get; set; }
    }
}