using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Contact;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _http;

        public ContactController(HttpClient http)
        {
            _http = http;
        }

        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ContactForm(ContactFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill out all fields.";
            }
            else
            {
                try
                {
                    var response = await _http.PostAsJsonAsync("https://localhost:7189/api/contact", viewModel);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Message sent successfully!";
                    }
                    else
                    {
                        TempData["Error"] = "An error occurred while sending the message.";
                    }
                }
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while sending the message.";
                    return RedirectToAction("Contact");
                }
            }
            return RedirectToAction("Contact");

        }
    }
}
