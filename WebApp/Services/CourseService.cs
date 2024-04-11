using Newtonsoft.Json;
using WebApp.ViewModels.Courses.Models;

namespace WebApp.Services;

public class CourseService(HttpClient http, IConfiguration config)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _config = config;

    public async Task<CourseResult> GetCoursesAsync(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _http.GetAsync($"https://localhost:7189/api/courses?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}&key=Co2QV2qViZLfdtCcx4A4FH4XrYtCJelpr94M92v4aIK6PunU4SWQHCsNEMyP623KkQRiGASAmsH0uXjdPDk2HyNtTC3SWkYAYLGKjWsdlQIBI9TQG9WHeTFw98bt7lCk");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());
                if (result != null)
                {
                    return result;
                }

            }
        }
        catch (Exception)
        {
            return null!;
        }
        return null!;
    }
}
