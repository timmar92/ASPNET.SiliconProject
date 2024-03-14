using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Newsletter;

public class NewsletterViewModel
{
    [Display(Name = "Daily Newsletter")]
    public bool DailyNews { get; set; }


    [Display(Name = "Advertising Updates")]
    public bool Advertising { get; set; }


    [Display(Name = "Week in Review")]
    public bool WeekReview { get; set; }


    [Display(Name = "Event Updates")]
    public bool Events { get; set; }


    [Display(Name = "Startups Weekly")]
    public bool Startups { get; set; }


    [Display(Name = "Podcasts")]
    public bool Podcasts { get; set; }


    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Your email")]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email")]
    [Required(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "You must select at least one checkbox.")]
    public bool AtLeastOneCheckbox
    {
        get
        {
            return DailyNews || Advertising || WeekReview || Events || Startups || Podcasts;
        }
    }
}
