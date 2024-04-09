using WebApp.ViewModels.Courses.Models;

namespace WebApp.ViewModels.Courses;

public class CourseDetailsViewModel
{
    public CourseModel Course { get; set; } = null!;
    public bool HasUserJoined { get; set; }
}
