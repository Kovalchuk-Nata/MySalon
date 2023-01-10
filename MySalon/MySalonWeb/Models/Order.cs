namespace MySalonWeb.Models
{
    public class Order
    {
        public int Id { get; set; } = 0!;
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public int ServiceId { get; set; }
        public Service? Services { get; set; }
        
        public DateTime OrderDateTime { get; set; }

    }
}
