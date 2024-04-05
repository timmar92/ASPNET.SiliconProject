using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using WebApp.ViewModels.Courses.Models;

namespace WebApp.Services;

public class CourseService(HttpClient http, IConfiguration config)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _config = config;

    public async Task<IEnumerable<CourseModel>> GetCoursesAsync(string category = "", string searchQuery = "")
    {
        var response = await _http.GetAsync($"https://localhost:7189/api/courses?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&key=Co2QV2qViZLfdtCcx4A4FH4XrYtCJelpr94M92v4aIK6PunU4SWQHCsNEMyP623KkQRiGASAmsH0uXjdPDk2HyNtTC3SWkYAYLGKjWsdlQIBI9TQG9WHeTFw98bt7lCk");

        if (response.IsSuccessStatusCode)
        {
            var courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync());
            return courses ??= null!;
            
        }
        return null!;
    }
}
