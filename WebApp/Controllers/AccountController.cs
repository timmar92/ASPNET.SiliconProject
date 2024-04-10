using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels.Account;
using WebApp.ViewModels.Courses;
using WebApp.ViewModels.Courses.Models;

namespace WebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly AddressManager _addressManager;
    private readonly UserCoursesManager _userCoursesManager;
    private readonly CourseService _courseService;
    private readonly CategoryService _categoryService;
    private readonly SignInManager<UserEntity> _signInManager;

    public AccountController(UserManager<UserEntity> userManager, AddressManager addressManager, UserCoursesManager userCoursesManager, CourseService courseService, CategoryService categoryService, SignInManager<UserEntity> signInManager)
    {
        _userManager = userManager;
        _addressManager = addressManager;
        _userCoursesManager = userCoursesManager;
        _courseService = courseService;
        _categoryService = categoryService;
        _signInManager = signInManager;
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
            FirstName = user!.FirstName,
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

    [HttpPost]
    public async Task<IActionResult> ChangePassword([Bind(Prefix = "ChangePasswordForm")] ChangePasswordViewModel viewModel)
    {
        var user = await _userManager.GetUserAsync(User);

        if (TryValidateModel(viewModel))
        {
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Password has been changed successfully";
                    return View(new ChangePasswordViewModel());
                }
            }

        }
        TempData["ErrorMessage"] = "Something went wrong, unable to change password";
        return View(viewModel);

    }


    [HttpPost]
    public async Task<IActionResult> DeleteAccount([Bind(Prefix = "DeleteAccountForm")] DeleteAccountViewModel viewModel)
    {
        var user = await _userManager.GetUserAsync(User);

        if (TryValidateModel(viewModel))
        {
            if (user != null)
            {
                _ = await _userCoursesManager.RemoveAllUserCourses(user.Id);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    TempData["SuccessMessage"] = "Account has been deleted successfully";
                    return RedirectToAction("Index", "Home");
                }
            }

        }
        TempData["ErrorMessage"] = "Something went wrong, unable to delete account";
        return View(viewModel);

    }

    #endregion

    #region saved courses

    [HttpGet]
    [Route("/account/saved-courses")]
    public async Task<IActionResult> SavedCourses(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {
        var user = await _userManager.GetUserAsync(User);
        var userCourses = await _userCoursesManager.GetUserCourses(user!.Id);

        var allCourses = await _courseService.GetCoursesAsync(category, searchQuery, pageNumber, pageSize);
        var joinedCourses = allCourses.Courses.Where(x => userCourses.Any(y => y.CourseId == x.Id)).ToList();

        var viewmodel = new AccountSavedCoursesViewModel
        {
            ProfileInfo = await PopulateProfileInfoAsync(),
            SavedCourses = new CourseIndexViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync(),
                Courses = joinedCourses,
                Pagination = new PaginationModel
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling((double)joinedCourses.Count / pageSize),
                    TotalItems = joinedCourses.Count
                },
                HasUserJoined = joinedCourses.ToDictionary(x => x.Id, x => true)
            }

        };



        return View(viewmodel);
    }


    public async Task<IActionResult> DeleteAllCourses()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var result = await _userCoursesManager.RemoveAllUserCourses(user.Id);
            if (result)
            {
                TempData["SuccessMessage"] = "All courses have been removed successfully";
                return RedirectToAction("SavedCourses");
            }
            TempData["ErrorMessage"] = "Something went wrong, unable to remove courses";
            return RedirectToAction("SavedCourses");
        }

        return RedirectToAction("SavedCourses");

    }


    #endregion
}


