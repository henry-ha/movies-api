using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MoviesWeb.Models
{
    [Table("User")]
    public class User : BaseEntity<int>
    {
        [Key]
        public override int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}