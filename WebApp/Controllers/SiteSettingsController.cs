using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class SiteSettingsController : Controller
{
    public IActionResult ChangeTheme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddMonths(2)
        };
        Response.Cookies.Append("ThemeMode", mode, option);
        return Ok();
    }

    [HttpPost]
    public IActionResult CookieConsent()
    {

    }
}
