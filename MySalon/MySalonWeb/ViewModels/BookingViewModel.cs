using MySalonWeb.Models;

namespace MySalonWeb.ViewModels
{
    public class BookingViewModel
    {
        public Client Client { get; set; }
        public Order Order { get; set; }
        public ServiceTypes ServiceType { get; set; }
        public string Time { get; set; } //timetable

    }
}
