﻿namespace MyContact_API.Models
{
    public class Salaries
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public string? Prenom { get; set; }
        public string? TelephoneFixe { get; set; }
        public string? TelephonePortable { get; set; }
        public string? Email { get; set; }

        // Foreign Key
        public int ServiceId { get; set; }
        public required Services Service { get; set; }

        public int SiteId { get; set; }
        public required Sites Site { get; set; }
    }
}
