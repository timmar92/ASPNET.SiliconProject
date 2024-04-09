namespace Infrastructure.Entities;

public class UserCoursesEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int? CourseId { get; set; }
}
