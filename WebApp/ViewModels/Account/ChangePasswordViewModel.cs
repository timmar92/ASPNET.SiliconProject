using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Account;

public class ChangePasswordViewModel
{
    [Display(Name = "Current password", Prompt = "Enter your current password", Order = 0)]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = null!;


    [Display(Name = "New password", Prompt = "Enter your new password", Order = 1)]
    [Required(ErrorMessage = "Invalid password")]
    [StringLength(50, ErrorMessage = "The password must be at least 8 characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,50}$", ErrorMessage = "Password needs one uppercase letter, one number and special character")]
    public string NewPassword { get; set; } = null!;


    [Display(Name = "Confirm new password", Prompt = "Confirm your new password", Order = 2)]
    [Required(ErrorMessage = "Password does not match")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
    public string ConfirmNewPassword { get; set; } = null!;
}
