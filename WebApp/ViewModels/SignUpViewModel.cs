using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class SignUpViewModel
{
    [Display(Name = "First Name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "Enter first name")]
    [MinLength(2, ErrorMessage = "First name is too short")]
    public string FirstName { get; set; } = null!;


    [Display(Name = "Last Name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Enter last name")]
    [MinLength(2, ErrorMessage = "Last name is too short")]
    public string LastName { get; set; } = null!;


    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email", Order = 2)]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email")]
    [Required(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
    [Required(ErrorMessage = "Invalid password")]
    [StringLength(50, ErrorMessage = "The password must be at least 8 characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,50}$", ErrorMessage = "Password needs one uppercase letter, one number and special character")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm Password", Prompt = "Confirm your password", Order = 4)]
    [Required(ErrorMessage = "Password does not match")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "I agree to the terms and conditions", Order = 5)]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms & conditions to sell your soul to our dark lord")]
    public bool TermsAndConditions { get; set; } = false;
}
