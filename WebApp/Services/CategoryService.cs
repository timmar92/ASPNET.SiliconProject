using Newtonsoft.Json;
using WebApp.ViewModels.Courses.Models;

namespace WebApp.Services;

public class CategoryService(HttpClient http, IConfiguration config)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _config = config;


    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
    {
        var response = await _http.GetAsync("https://localhost:7189/api/categories?key=Co2QV2qViZLfdtCcx4A4FH4XrYtCJelpr94M92v4aIK6PunU4SWQHCsNEMyP623KkQRiGASAmsH0uXjdPDk2HyNtTC3SWkYAYLGKjWsdlQIBI9TQG9WHeTFw98bt7lCk");

        if (response.IsSuccessStatusCode)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(await response.Content.ReadAsStringAsync());
            return categories ??= null!;
        }
        return null!;
    }
}
