namespace MyContact_API.Models
{
    public class Services
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public ICollection<Salaries>? Salaries { get; set; }
    }
}
