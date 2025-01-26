using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Models
{
    [Table("refresh_tokens")]
    public class RefreshToken
    {
        [Key]
        [Column("id")]
        [Required]
        public Guid Id { get; set; }

        [Column("user_id")]
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
