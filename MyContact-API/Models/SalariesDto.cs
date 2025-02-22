namespace MyContact_API.Models
{
    public class SalariesDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string? Prenom { get; set; }
        public string? TelephoneFixe { get; set; }
        public string? TelephonePortable { get; set; }
        public string? Email { get; set; }
        public string? ServiceNom { get; set; }   
        public string? SiteVille { get; set; }
        public int ServiceId { get; internal set; }
        public int SiteId { get; internal set; }
    }
}
