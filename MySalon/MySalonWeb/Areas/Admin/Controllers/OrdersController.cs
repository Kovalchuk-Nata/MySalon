
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySalonWeb.Domain;
using MySalonWeb.Models;

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
            var orders = salonDb.Orders;
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
        public IActionResult Create(Order obj)
        {
            if (ModelState.IsValid)
            {
                salonDb.Orders.Add(obj);
                salonDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
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

    }
}
