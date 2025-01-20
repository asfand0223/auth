using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        [Required]
        public Guid Id { get; set; }

        [Column("username")]
        [Required]
        public required string Username { get; set; }

        [Required]
        [Column("password")]
        public required string Password { get; set; }

        [Required]
        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
    }
}
