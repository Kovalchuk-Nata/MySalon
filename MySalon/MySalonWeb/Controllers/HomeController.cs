using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySalonWeb.Domain;
using MySalonWeb.Models;
using MySalonWeb.ViewModels;
using System.Diagnostics;
using System.Linq;
using static System.Collections.Specialized.BitVector32;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/about")]
        public IActionResult About()
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



    }
}