﻿namespace MyContact_API.Models
{
    public class SalariesDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string? TelephoneFixe { get; set; }
        public string? TelephonePortable { get; set; }
        public string? Email { get; set; }
        public int? ServiceId { get; set; }
        public string? ServiceNom { get; set; }
        public int? SiteId { get; set; }
        public string? SiteVille { get; set; }
    }
}
