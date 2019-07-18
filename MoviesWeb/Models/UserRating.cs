using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesWeb.Models
{
    [Table("UserRating")]
    public class UserRating : BaseEntity<int>
    {
        public override int Id { get; set; }

        public int User_Id { get; set; }
        public int Movie_Id { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        
        [ForeignKey("User_Id")]
        public virtual User User { get; set; }

        [ForeignKey("Movie_Id")]
        public virtual Movie Movie { get; set; }
    }
}