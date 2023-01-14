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
using Microsoft.EntityFrameworkCore;

namespace MySalonWeb.Controllers
{
    public class BookingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SalonContext salonDb;


        public BookingController(ILogger<HomeController> logger, SalonContext salonContext)
        {
            _logger = logger;
            salonDb = salonContext;
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
            var date = Convert.ToDateTime(bookingViewModel.Date);
            bookingViewModel.Date = date.ToString("dd.MM.yyyy");

            if (ModelState.IsValid)
            {
                Client client = bookingViewModel.Client;
                var clientId = salonDb.Clients.FirstOrDefault(s => s.Phone == client.Phone)?.Id;
                if (clientId == null)
                {
                    salonDb.Clients.Add(client);
                    salonDb.SaveChanges();
                }

                Order order = bookingViewModel.Order;
                order.ClientId = clientId ?? client.Id;
                order.OrderDate = Convert.ToDateTime(bookingViewModel.Date);
                salonDb.Orders.Add(order);
                salonDb.SaveChanges();

                return RedirectToAction("ConfirmOrder", new { OrderId = order.Id });
            }
            return View(bookingViewModel);
        }

        [Route("/confirmOrder")]
        //[HttpGet]
        public IActionResult ConfirmOrder(string OrderId)
        {
            var orderList = salonDb.Orders.Where(o => o.Id == Int32.Parse(OrderId)).Include(o => o.Client).Include(o => o.Services).ToList();
            var order = orderList.FirstOrDefault(o => o.Id == Int32.Parse(OrderId));

            return View(order);
        }


        [HttpGet, HttpPost]
        public PartialViewResult? GetConfigForm(string id)
        {

            ViewBag.ServiceDictionary = new SelectList(salonDb.Services.Where(s => (int)s.ServiceType == Int32.Parse(id)), "Id", "ServiceName");

            return PartialView("_PartViewBooking1");
        }

        [HttpGet, HttpPost]
        public PartialViewResult? GetTimeAndPrice(string id, string date)
        {
            // ловим цену на услугу 
            ViewBag.Price = salonDb.Services.FirstOrDefault(s => s.Id == Int32.Parse(id))?.Price;
            ViewBag.ServiceName = salonDb.Services.FirstOrDefault(s => s.Id == Int32.Parse(id))?.ServiceName;
            ViewBag.Date = date;

            // массив со всеми записями
            var dateBooking = Convert.ToDateTime(date);
            var listOfDate = salonDb.Orders.Where(s => (int)s.ServiceId == Int32.Parse(id) && s.OrderDate == dateBooking).ToList();

            // создаем масив время
            int period = ((int)(salonDb.Services.FirstOrDefault(s => s.Id == Int32.Parse(id))!.Period));
            int startTime = 10;
            int countTime = 10 / period;

            var arrayOfTime = new int[countTime];
            for (int i = 0; i < countTime; i++)
            {
                arrayOfTime[i] = startTime;
                startTime += period;
            }
            var matchedItems = arrayOfTime.ToList();
            List<int> result = arrayOfTime.ToList();

            foreach (var i in matchedItems)
            {
                
                if (listOfDate.Exists(item => item.OrderTime == i) == true)
                {
                    result.Remove(i);
                }
                else { continue; }
            }

            var items = result.Select(item => new SelectListItem
            {
                Text = item.ToString(),
                Value = item.ToString()
            });

            ViewBag.TimeDictionary = items;
            return PartialView("_PartViewBooking2");
        }


    }
}