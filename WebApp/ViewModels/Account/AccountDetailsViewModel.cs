namespace WebApp.ViewModels.Account;

public class AccountDetailsViewModel
{
    public ProfileInfoViewModel ProfileInfo { get; set; } = null!;
    public BasicInfoViewModel BasicInfo { get; set; } = null!;
    public AddressInfoViewModel AddressInfo { get; set; } = null!;

}
