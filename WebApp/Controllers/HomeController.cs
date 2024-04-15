using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.ViewModels.Newsletter;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IActionResult Index()
    {
        return View();
    }


    [Route("/errorpage")]
    public IActionResult ErrorPage()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Subscribe(NewsletterViewModel viewModel)
    {

        if (!viewModel.AtLeastOneCheckbox)
        {
            TempData["ErrorMessage"] = "You must select at least one checkbox.";
            return new RedirectResult(Url.Action("Index", "Home") + "#newsletter");
        }

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "you must fill out the form correctly";
            return new RedirectResult(Url.Action("Index", "Home") + "#newsletter");
        }

        var response = await _httpClient.PostAsJsonAsync("https://localhost:7189/api/subscribers?key=Co2QV2qViZLfdtCcx4A4FH4XrYtCJelpr94M92v4aIK6PunU4SWQHCsNEMyP623KkQRiGASAmsH0uXjdPDk2HyNtTC3SWkYAYLGKjWsdlQIBI9TQG9WHeTFw98bt7lCk", viewModel);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "You have successfully subscribed!";
            return new RedirectResult(Url.Action("Index", "Home") + "#newsletter");
        }
        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            TempData["ErrorMessage"] = "You are already subscribed";
            return new RedirectResult(Url.Action("Index", "Home") + "#newsletter");
        }

        return new RedirectResult(Url.Action("Index", "Home") + "#newsletter");
    }
}
