
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySalonWeb.Domain;
using MySalonWeb.Models;
using MySalonWeb.ViewModels;

namespace MySalonWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly SalonContext salonDb;

        public OrdersController(SalonContext db)
        {
            salonDb = db;
        }

        public ActionResult Index()
        {
            var orders = salonDb.Orders.Include(o => o.Services);
            return View(orders);
        }

        //GET
        //[Route("[action]")]
        public IActionResult Create()
        {
            return View();
        }

        //POST
        //[Route("[action]")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(BookingViewModel bookingViewModel)
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
                salonDb.Orders.Add(bookingViewModel.Order);
                salonDb.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bookingViewModel);
           
        }

        //GET
        //[Route("[action]")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var orderId = salonDb.Orders.FirstOrDefault(c => c.Id == id); 

            if (orderId == null)
            {
                return NotFound();
            }

            return View(orderId);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Order obj)
        {
            if (ModelState.IsValid)
            {
                salonDb.Orders.Update(obj);  
                salonDb.SaveChanges(true);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var order = salonDb.Orders.FirstOrDefault(c => c.Id == id);
             
            return View(order);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = salonDb.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            salonDb.Orders.Remove(obj);
            salonDb.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet, HttpPost]
        public PartialViewResult? GetConfigForm(string id)
        {

            ViewBag.ServiceDictionary = new SelectList(salonDb.Services.Where(s => (int)s.ServiceType == Int32.Parse(id)), "Id", "ServiceName");

            return PartialView("_PartViewCreate1");
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
            return PartialView("_PartViewCreate2");
        }

    }

}

