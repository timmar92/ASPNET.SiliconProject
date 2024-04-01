using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.ViewModels.Courses;
using WebApp.ViewModels.Courses.Models;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly HttpClient _http;

    public CoursesController(HttpClient httpClient)
    {
        _http = httpClient;
    }



    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = new CourseIndexViewModel();

        var response = await _http.GetAsync("https://localhost:7189/api/courses?key=Co2QV2qViZLfdtCcx4A4FH4XrYtCJelpr94M92v4aIK6PunU4SWQHCsNEMyP623KkQRiGASAmsH0uXjdPDk2HyNtTC3SWkYAYLGKjWsdlQIBI9TQG9WHeTFw98bt7lCk");

        viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;

        return View(viewModel);
    }




    public IActionResult Details(int id)
    {
        var viewModel = new CourseModel();

        var response = _http.GetAsync($"https://localhost:7189/api/courses/{id}?key=Co2QV2qViZLfdtCcx4A4FH4XrYtCJelpr94M92v4aIK6PunU4SWQHCsNEMyP623KkQRiGASAmsH0uXjdPDk2HyNtTC3SWkYAYLGKjWsdlQIBI9TQG9WHeTFw98bt7lCk").Result;

        viewModel = JsonConvert.DeserializeObject<CourseModel>(response.Content.ReadAsStringAsync().Result)!;

        return View(viewModel);
    }
}
