using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;
    private readonly AccountService _accountService;

    public AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AccountService accountService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _accountService = accountService;
    }

    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("SignIn", "Auth");
        }

        var userEntity = await _userManager.GetUserAsync(User);

        var viewModel = new AccountDetailsViewModel()
        {
            User = userEntity!
        };
        return View(viewModel);
    }


    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> BasicInfo(AccountDetailsViewModel viewModel)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser!.Email != viewModel.User.Email)
        {
            var existingUser = await _userManager.FindByEmailAsync(viewModel.User.Email!);
            if (existingUser != null)
            {
                ModelState.AddModelError("EmailExists", "Email already exists");
                ViewData["ErrorMessage"] = "Email already exists";
                return View("Details", viewModel);
            }
        }

        var result = await _accountService.UpdateInfoAsync(viewModel.User);
        if (result == null)
        {
            ModelState.AddModelError("UserNotFound", "User not found");
            ViewData["ErrorMessage"] = "User not found";
            return View("Details", viewModel);
        }

        return RedirectToAction("Details", "Account");
    }

}


