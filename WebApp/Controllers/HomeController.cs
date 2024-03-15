using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


    [Route("/errorpage")]
    public IActionResult ErrorPage()
    {
        return View();
    }

}
