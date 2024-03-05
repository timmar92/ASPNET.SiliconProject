using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Details()
        {
            return View();
        }
    }
}
