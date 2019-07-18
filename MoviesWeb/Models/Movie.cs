using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MoviesWeb.Models
{
    [Table("Movie")]
    public class Movie : BaseEntity<int>
    {
        [Key]
        public override int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int YearOfRelease { get; set; }

        [Required]
        [StringLength(255)]
        public string Genre { get; set; }
    }
}