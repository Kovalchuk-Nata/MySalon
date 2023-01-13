
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySalonWeb.Domain;
using MySalonWeb.Models;

namespace MySalonWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly SalonContext salonDb;

        public ClientsController(SalonContext db)
        {
            salonDb = db;
        }

        public ActionResult Index()
        {
            var orders = salonDb.Clients;
            return View(orders);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Client obj)
        {
            if (ModelState.IsValid)
            {
                salonDb.Clients.Add(obj);
                salonDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var clientId = salonDb.Clients.FirstOrDefault(c => c.Id == id); 

            if (clientId == null)
            {
                return NotFound();
            }

            return View(clientId);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Client obj)
        {
            if (ModelState.IsValid)
            {
                salonDb.Clients.Update(obj);  
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
            var client = salonDb.Clients.FirstOrDefault(c => c.Id == id);
             
            return View(client);
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
