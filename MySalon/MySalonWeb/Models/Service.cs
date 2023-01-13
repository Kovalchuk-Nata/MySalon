namespace MySalonWeb.Models
{
    public class Service
    {
        public int Id { get; set; }
        public ServiceTypes ServiceType { get; set; } //enum
        public string ServiceName { get; set; }
        public int Price { get; set; }
        public int Period { get; set; } 

        public List<Order>? Orders { get; set; }

    }

   
}
