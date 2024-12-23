namespace MyContact_API.Models
{
    public class Sites
    {
        public int Id { get; set; }
        public string? Ville { get; set; }
        public required ICollection<Salaries> Salaries { get; set; }
    }
}
