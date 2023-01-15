
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySalonWeb.Domain;
using MySalonWeb.Models;

namespace MySalonWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ExpertsController : Controller
    {
        private readonly SalonContext salonDb;

        public ExpertsController(SalonContext db)
        {
            salonDb = db;
        }

        public ActionResult Index()
        {
            var orders = salonDb.Experts;
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
        public IActionResult Create(Expert obj)
        {
            if (ModelState.IsValid)
            {
                salonDb.Experts.Add(obj);
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
            var expertId = salonDb.Experts.FirstOrDefault(c => c.Id == id); 

            if (expertId == null)
            {
                return NotFound();
            }

            return View(expertId);
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Expert obj)
        {
            if (ModelState.IsValid)
            {
                salonDb.Experts.Update(obj);  
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
            var expert = salonDb.Experts.FirstOrDefault(c => c.Id == id);
             
            return View(expert);
        }


        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = salonDb.Experts.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            salonDb.Experts.Remove(obj);
            salonDb.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
