﻿using System.ComponentModel.DataAnnotations;
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
    }
}
