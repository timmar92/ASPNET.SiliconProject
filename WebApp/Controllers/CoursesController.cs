using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserCoursesManager _userCoursesManager;
    private readonly UserManager<UserEntity> _userManager;

    public CoursesController(HttpClient http, CategoryService categoryService, CourseService courseService, UserCoursesManager userCoursesManager, UserManager<UserEntity> userManager)
    {
        _http = http;
        _categoryService = categoryService;
        _courseService = courseService;
        _userCoursesManager = userCoursesManager;
        _userManager = userManager;
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
            },
            HasUserJoined = new Dictionary<int, bool>()

        };

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            foreach (var course in viewModel.Courses)
            {
                var hasUserJoined = await _userCoursesManager.IsUserJoined(user.Id, course.Id);
                viewModel.HasUserJoined.Add(course.Id, hasUserJoined);
            }
        }


        return View(viewModel);
    }




    public async Task<IActionResult> Details(int id)
    {
        var viewModel = new CourseDetailsViewModel
        {
            HasUserJoined = new bool()
        };

        var response = _http.GetAsync($"https://localhost:7189/api/courses/{id}?key=Co2QV2qViZLfdtCcx4A4FH4XrYtCJelpr94M92v4aIK6PunU4SWQHCsNEMyP623KkQRiGASAmsH0uXjdPDk2HyNtTC3SWkYAYLGKjWsdlQIBI9TQG9WHeTFw98bt7lCk").Result;

        viewModel.Course = JsonConvert.DeserializeObject<CourseModel>(response.Content.ReadAsStringAsync().Result)!;

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var IsUserJoined = _userCoursesManager.IsUserJoined(user.Id, viewModel.Course.Id);
            if (IsUserJoined.Result == true)
            {
                viewModel.HasUserJoined = true;
            }

        }

        return View(viewModel);
    }


    public async Task<IActionResult> JoinCourse(int courseId)
    {


        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            var result = await _userCoursesManager.AddUserCourse(user.Id, courseId);
            if (result == true)
            {
                TempData["SuccessMessage"] = "You have successfully joined the course";
                return RedirectToAction("Index");
            }

        }
        TempData["ErrorMessage"] = "An error occurred while trying to join the course";
        return RedirectToAction("Index");

    }

    public async Task<IActionResult> LeaveCourse(int courseId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var result = await _userCoursesManager.RemoveUserCourse(user.Id, courseId);
            if (result == true)
            {
                TempData["SuccessMessage"] = "You have successfully left the course";
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }
        TempData["ErrorMessage"] = "An error occurred while trying to leave the course";
        return Redirect(Request.Headers["Referer"].ToString());
    }
}
