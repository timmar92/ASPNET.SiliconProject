﻿using WebApp.ViewModels.Courses.Models;

namespace WebApp.ViewModels.Courses;

public class CourseIndexViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = [];
    public IEnumerable<CategoryModel>? Categories { get; set; }
    public PaginationModel? Pagination { get; set; }

    public Dictionary<int, bool> HasUserJoined { get; set; } = null!;
}
