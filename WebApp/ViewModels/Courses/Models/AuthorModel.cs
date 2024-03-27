namespace WebApp.ViewModels.Courses.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? YoutubeUrl { get; set; }
        public string? FacebookUrl { get; set; }

        public int CourseEntityId { get; set; }
    }
}
