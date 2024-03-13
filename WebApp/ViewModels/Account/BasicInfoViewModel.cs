using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Account;

public class BasicInfoViewModel
{
    public string UserId { get; set; } = null!;



    [Required(ErrorMessage = "A valid first name is required")]
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name")]
    [MinLength(2, ErrorMessage = "First name is too short")]
    public string FirstName { get; set; } = null!;



    [Required(ErrorMessage = "A valid last name is required")]
    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Enter your last name")]
    [MinLength(2, ErrorMessage = "Last name is too short")]
    public string LastName { get; set; } = null!;



    [Required(ErrorMessage = "A valid email address is required")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address")]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;



    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone (optional)", Prompt = "Enter your phone number")]
    public string? PhoneNumber { get; set; }



    [DataType(DataType.MultilineText)]
    [Display(Name = "Bio (optional)", Prompt = "Add a short bio...")]
    public string? Biography { get; set; }

    public bool IsExternalAccount { get; set; }

}
