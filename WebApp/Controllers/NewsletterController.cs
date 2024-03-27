using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Newsletter;

namespace WebApp.Controllers;

public class NewsletterController : Controller
{
    public IActionResult Subscribe(NewsletterViewModel viewModel)
    {


        return View();
    }
}
