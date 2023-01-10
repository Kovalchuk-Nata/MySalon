namespace MySalonWeb.Models
{
    public class Expert
    {
        public int Id { get; set; } = 0!;
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public int Phone { get; set; }
        public ServiceTypes ServiceType { get; set; } //enum

    }
}
