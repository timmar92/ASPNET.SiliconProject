using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Account;

namespace WebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly AddressManager _addressManager;

    public AccountController(UserManager<UserEntity> userManager, AddressManager addressManager)
    {
        _userManager = userManager;
        _addressManager = addressManager;
    }


    #region [HttpGet] Details
    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel
        {
            ProfileInfo = await PopulateProfileInfoAsync()
        };
        viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfo ??= await PopulateAddressInfoAsync();
        return View(viewModel);
    }
    #endregion


    #region [HttpPost] Update Details

    [HttpPost]
    public async Task<IActionResult> DetailsBasic([Bind(Prefix = "BasicInfo")] BasicInfoViewModel viewModel)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            viewModel.UserId = user.Id;

            if (TryValidateModel(viewModel))
            {
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.Email = viewModel.Email;
                user.PhoneNumber = viewModel.PhoneNumber;
                user.Biography = viewModel.Biography;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("IncorrectValues", "Something went wrong, unable to save data");
                }
            }
        }



        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("IncorrectValues", "Something went wrong, unable to save data");
            ViewData["ErrorMessage"] = "Something went wrong, unable to save data";
            var accountDetailsViewModel = new AccountDetailsViewModel
            {
                BasicInfo = viewModel,
                ProfileInfo = await PopulateProfileInfoAsync(),
                AddressInfo = await PopulateAddressInfoAsync()
            };
            return View("Details", accountDetailsViewModel);
        }

        return RedirectToAction("Details");
    }

    [HttpPost]
    public async Task<IActionResult> DetailsAddress([Bind(Prefix = "AddressInfo")] AddressInfoViewModel viewModel)
    {
        var user = await _userManager.GetUserAsync(User);


        if (TryValidateModel(viewModel))
        {

            if (user != null)
            {
                var address = await _addressManager.GetAddressAsync(user.Id);
                if (address != null)
                {
                    address.AddressLine_1 = viewModel.AddressLine_1;
                    address.AddressLine_2 = viewModel.AddressLine_2;
                    address.PostalCode = viewModel.PostalCode;
                    address.City = viewModel.City;

                    var result = await _addressManager.UpdateAddressAsync(address);
                    if (!result)
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wrong, unable to save data");
                    }
                }
                else
                {
                    address = new AddressEntity
                    {
                        UserId = user.Id,
                        AddressLine_1 = viewModel.AddressLine_1,
                        AddressLine_2 = viewModel.AddressLine_2,
                        PostalCode = viewModel.PostalCode,
                        City = viewModel.City
                    };

                    var result = await _addressManager.CreateAddressAsync(address);
                    if (!result)
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wrong, unable to save data");
                    }
                }
            }
        }

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("IncorrectValues", "Something went wrong, unable to save data");
            ViewData["ErrorMessage"] = "Something went wrong, unable to save data";
            var accountDetailsViewModel = new AccountDetailsViewModel
            {
                AddressInfo = viewModel,
                ProfileInfo = await PopulateProfileInfoAsync(),
                BasicInfo = await PopulateBasicInfoAsync()
            };
            return View("Details", accountDetailsViewModel);
        }

        return RedirectToAction("Details");
    }
    #endregion


    #region populate methods
    private async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileInfoViewModel
        {
            FirstName = user!.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            IsExternalAccount = user.IsExternalAccount
        };
    }


    private async Task<BasicInfoViewModel> PopulateBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new BasicInfoViewModel
        {
            //UserId = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            Biography = user.Biography,
            IsExternalAccount = user.IsExternalAccount
        };
    }


    private async Task<AddressInfoViewModel> PopulateAddressInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var address = await _addressManager.GetAddressAsync(user.Id);
            if (address != null)
            {
                return new AddressInfoViewModel
                {
                    AddressLine_1 = address.AddressLine_1,
                    AddressLine_2 = address.AddressLine_2,
                    PostalCode = address.PostalCode,
                    City = address.City
                };
            }
        }

        return new AddressInfoViewModel();
    }


    #endregion


    #region Security

    [HttpGet]
    [Route("/account/security")]
    public async Task<IActionResult> Security()
    {
        var viewModel = new AccountSecurityViewModel
        {
            ProfileInfo = await PopulateProfileInfoAsync()
        };
        return View(viewModel);
    }

    #endregion
}


