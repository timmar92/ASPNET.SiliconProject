namespace WebApp.ViewModels.Courses.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ArticleNumber { get; set; }
        public string? DownloadResource { get; set; }
        public string? Price { get; set; }
        public string? DiscountPrice { get; set; }
        public string? Hours { get; set; }
        public bool IsBestSeller { get; set; }
        public string? LikesInNumbers { get; set; }
        public string? LikesInPercent { get; set; }



        public PointListModel? PointList { get; set; }
        public DetailsListModel? DetailsList { get; set; }
        public AuthorModel? Author { get; set; }
        public ReviewsModel? Reviews { get; set; }
    }
}
