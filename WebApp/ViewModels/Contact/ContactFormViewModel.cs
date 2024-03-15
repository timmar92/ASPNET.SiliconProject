using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Contact;

public class ContactFormViewModel
{
    [Display(Name = "Full Name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "a name is required")]
    [MinLength(2, ErrorMessage = "The name is too short")]
    public string FullName { get; set; } = null!;



    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email address", Order = 1)]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email")]
    [Required(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;



    [Display(Name = "Service (optional)", Prompt = "Choose the service you are interested in", Order = 2)]
    public string? SelectedOption { get; set; }



    [DataType(DataType.MultilineText)]
    [Display(Name = "Message", Prompt = "Enter your message here...", Order = 3)]
    [MinLength(20, ErrorMessage = "The message is too short")]
    public string Message { get; set; } = null!;

}
