﻿using WebApp.ViewModels.Courses.Models;

namespace WebApp.ViewModels.Courses;

public class CourseIndexViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = [];
}