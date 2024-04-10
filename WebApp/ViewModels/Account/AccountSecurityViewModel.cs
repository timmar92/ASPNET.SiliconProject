namespace WebApp.ViewModels.Account;

public class AccountSecurityViewModel
{
    public ProfileInfoViewModel ProfileInfo { get; set; } = null!;

    public ChangePasswordViewModel ChangePasswordForm { get; set; } = null!;

    public DeleteAccountViewModel deleteAccountform { get; set; } = null!;
}
