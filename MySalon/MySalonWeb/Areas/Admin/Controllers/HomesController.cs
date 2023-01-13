using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MySalonWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomesController : Controller
    {
        // GET: HomesController
        public ActionResult Index()
        {
            return View();
        }

    }
}
