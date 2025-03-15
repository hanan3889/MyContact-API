using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyContact_API.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(250)]
        public string Email { get; set; }

        [Required]
        [MaxLength(250)]
        public string Password { get; set; }

        public int Roles { get; set; }
        [Range(1000, 9999, ErrorMessage = "Le code secret doit être un nombre à 4 chiffres.")]
        public int SecretCode { get; set; }
    }
}
