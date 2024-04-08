using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Services;
using WebApp.ViewModels.Courses;
using WebApp.ViewModels.Courses.Models;

namespace WebApp.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly HttpClient _http;
    private readonly CategoryService _categoryService;
    private readonly CourseService _courseService;

    public CoursesController(HttpClient http, CategoryService categoryService, CourseService courseService)
    {
        _http = http;
        _categoryService = categoryService;
        _courseService = courseService;

    }

    [HttpGet]
    public async Task<IActionResult> Index(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {
        var courseResult = await _courseService.GetCoursesAsync(category, searchQuery, pageNumber, pageSize);

        var viewModel = new CourseIndexViewModel
        {
            Categories = await _categoryService.GetCategoriesAsync(),
            Courses = courseResult.Courses,
            Pagination = new PaginationModel
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = courseResult.TotalPages,
                TotalItems = courseResult.TotalItems
            }
        };


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
