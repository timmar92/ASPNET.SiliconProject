namespace WebApp.ViewModels.Courses.Models;

public class CourseResult
{
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<CourseModel> Courses { get; set; } = [];
}
