using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Account;

public class AddressInfoViewModel
{
    [Required(ErrorMessage = "An Address is required")]
    [DataType(DataType.Text)]
    [Display(Name = "Address line 1", Prompt = "Enter your first address line")]
    public string AddressLine_1 { get; set; } = null!;



    [DataType(DataType.Text)]
    [Display(Name = "Address line 2", Prompt = "Enter your second address line")]
    public string? AddressLine_2 { get; set; }



    [Required(ErrorMessage = "A postal code is required")]
    [DataType(DataType.Text)]
    [Display(Name = "Postal code", Prompt = "Enter your postal code")]
    public string PostalCode { get; set; } = null!;



    [Required(ErrorMessage = "A valid first name is required")]
    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "Enter your city")]
    public string City { get; set; } = null!;
}
