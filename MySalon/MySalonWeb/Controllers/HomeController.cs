using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySalonWeb.Domain;
using MySalonWeb.Models;
using MySalonWeb.ViewModels;
using System.Diagnostics;
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
            salonDb.Clients.Add(bookingViewModel.Client);
            salonDb.Orders.Add(bookingViewModel.Order);
            salonDb.SaveChanges();

            return View("booking");
        }

        //[HttpPost]
        //public IActionResult Booking(BookingModel model)
        //{
        //    Debug.WriteLine(model.Name);
        //    Debug.WriteLine(model.Surename);
        //    Debug.WriteLine(model.Phone);
        //    Debug.WriteLine(model.Email);
        //    Debug.WriteLine(model.Service);
        //    Debug.WriteLine(model.Date);

        //    return View("Success");
        //}


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
        public PartialViewResult? GetTime(string id)
        {

            ViewBag.ServiceDictionary = new SelectList(salonDb.Services.Where(s => (int)s.ServiceType == Int32.Parse(id)), "Id", "ServiceName");

            return PartialView("_PartViewBooking1");
        }

    }
}