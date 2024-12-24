using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public enum ParentalRating
    {
        Unrestrained,
        A10,
        A12,
        A14,
        A16,
        A18
    }

    [Table("movies")]
    public class Movie
    {
        [Column("id"), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("name", TypeName = "varchar(100)"), Required]
        public string Name { get; set; }

        [Column("parental_rating"), Required]
        public ParentalRating ParentalRating { get; set; }

        [Column("movie_theater_id"), Required]
        public long MovieTheaterId { get; set; }


        public MovieTheater MovieTheater { get; set; }

        public ICollection<MovieSession> Sessions { get; set; }
    }
}
