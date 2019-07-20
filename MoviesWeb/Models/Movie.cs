using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MoviesWeb.Models
{
    [Table("Movie")]
    public class Movie : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int YearOfRelease { get; set; }

        [Required]
        [StringLength(255)]
        public string Genres { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal AverageRating { get; set; }
    }
}