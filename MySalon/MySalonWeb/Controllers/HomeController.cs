using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySalonWeb.Domain;
using MySalonWeb.Models;
using MySalonWeb.ViewModels;
using System.Diagnostics;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace MySalonWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SalonContext salonDb;


        public HomeController(ILogger<HomeController> logger, SalonContext salonContext)
        {
            _logger = logger;
            salonDb = salonContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/about")]
        public IActionResult About()
        {
            return View();
        }

        [Route("/booking")]
        public IActionResult Booking()
        {
            return View();
        }

        [Route("/booking")]
        [HttpPost]
        public IActionResult Booking(BookingViewModel bookingViewModel)
        {
            Client client = bookingViewModel.Client;
            salonDb.Clients.Add(client);
            salonDb.SaveChanges();

            Order order = bookingViewModel.Order;
            order.ClientId = client.Id;
            order.OrderDate = Convert.ToDateTime(bookingViewModel.Date);
            salonDb.Orders.Add(bookingViewModel.Order);
            salonDb.SaveChanges();

            return View("booking");
        }

        [Route("/booking/order")]
        [HttpPost]
        public IActionResult Order(BookingViewModel bookingViewModel)
        {
            return View();
        }


        [Route("/experts")]
        public IActionResult Experts()
        {
            return View();
        }

        [Route("/contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("/portfolio")]
        public IActionResult Portfolio()
        {
            return View();
        }

        [Route("/service")]
        public IActionResult Service()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //-------------------------------------------//



        [HttpGet, HttpPost]
        public PartialViewResult? GetConfigForm(string id)
        {

            ViewBag.ServiceDictionary = new SelectList(salonDb.Services.Where(s => (int)s.ServiceType == Int32.Parse(id)), "Id", "ServiceName");

            return PartialView("_PartViewBooking1");
        }

        [HttpGet, HttpPost]
        public PartialViewResult? GetTimeAndPrice2(string id, string date)
        {

                                
            ViewBag.Price =  from m in salonDb.Services where m.Id == Int32.Parse(id) select m.Price;

            // Convert.ToDateTime(bookingViewModel.Date)

            ViewBag.Date = date;

            return PartialView("_PartViewBooking2");
        }

        [HttpGet, HttpPost]
        public PartialViewResult? GetTimeAndPrice(string id, string date)
        {

            ViewBag.Price = salonDb.Services.FirstOrDefault(s => s.Id == Int32.Parse(id))?.Price; // ловим цену на услугу
            ViewBag.Date = date;

            var dateBooking = Convert.ToDateTime(date);

            var listOfDate = salonDb.Orders.Where(s => (int)s.ServiceId == Int32.Parse(id) && s.OrderDate == dateBooking).ToList();

            return PartialView("_PartViewBooking2");
        }


        /*public GetTimeTable(int date, int period)
        {
            GetTimeTable();
        period + 10
            ViewBag.TimeDictionary = new SelectList(salonDb.Services.Where(s => (int)s.ServiceType == Int32.Parse(id)), "Id", "ServiceTime");
        }
        */

    }
}