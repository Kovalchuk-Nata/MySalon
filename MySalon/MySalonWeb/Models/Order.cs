using System.ComponentModel.DataAnnotations;

namespace MySalonWeb.Models
{
    public class Order
    {
        public int Id { get; set; } = 0!;
        
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public int ServiceId { get; set; }
        public Service? Services { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required]
        public int OrderTime { get; set; }
    }
}
