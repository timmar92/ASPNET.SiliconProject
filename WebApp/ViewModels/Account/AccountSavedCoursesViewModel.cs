using WebApp.ViewModels.Courses;

namespace WebApp.ViewModels.Account
{
    public class AccountSavedCoursesViewModel
    {
        public ProfileInfoViewModel ProfileInfo { get; set; } = null!;
        public CourseIndexViewModel SavedCourses { get; set; } = null!;
    }
}
