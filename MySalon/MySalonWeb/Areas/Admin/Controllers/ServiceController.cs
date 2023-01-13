
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySalonWeb.Domain;
using MySalonWeb.Models;

namespace MySalonWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly SalonContext salonDb;

        public ServiceController(SalonContext db)
        {
            salonDb = db;
        }

        public ActionResult Index()
        {
            var services = salonDb.Services;
            return View(services);
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
        public IActionResult Create(Service obj)
        {
            if (ModelState.IsValid)
            {
                salonDb.Services.Add(obj);
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
            var serviseId = salonDb.Services.FirstOrDefault(c => c.Id == id); 

            if (serviseId == null)
            {
                return NotFound();
            }

            return View(serviseId);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Service obj)
        {
            if (ModelState.IsValid)
            {
                salonDb.Services.Update(obj);  
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
            var service = salonDb.Services.FirstOrDefault(c => c.Id == id);
             
            return View(service);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = salonDb.Services.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            salonDb.Services.Remove(obj);
            salonDb.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
