﻿using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<UserEntity> _userManager;

    public AuthController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        return View();
    }



    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var Exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Email);
            if (Exists)
            {
                ModelState.AddModelError("AlreadyExists", "User with the same email address already exists");
                ViewData["ErrorMessage"] = "User with the same email address already exists";
                return View(viewModel);
            }

            var userEntity = new UserEntity
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                UserName = viewModel.Email
            };

            var result = await _userManager.CreateAsync(userEntity, viewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            }
        }

        return View(viewModel);
    }




    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        return View();
    }


    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (ModelState.IsValid) 
        {

        }
        
        ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
        ViewData["ErrorMessage"] = "Incorrect email or password";
        return View(viewModel);
    }


}
