using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Account;

public class DeleteAccountViewModel
{
    [Display(Name = "Yes, I want to delete my account")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms & conditions to sell your soul to our dark lord")]
    public bool DeleteAccount { get; set; } = false;
}
