using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesWeb.Models
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}