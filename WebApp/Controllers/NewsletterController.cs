using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class NewsletterController : Controller
{
    public IActionResult Subscribe()
    {
        return View();
    }
}
