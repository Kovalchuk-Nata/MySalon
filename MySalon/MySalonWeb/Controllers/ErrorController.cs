using Microsoft.AspNetCore.Mvc;

namespace MySalonWeb.Controllers
{
    public class ErrorController : Controller
    {


        [Route("/Error")]
        public ActionResult Error()
        {
            return View();
        }

    }
}
