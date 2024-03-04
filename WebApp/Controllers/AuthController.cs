using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        return View();
    }



    //[HttpPost]
    //[Route("/signup")]
    //public IActionResult SignUp()
    //{
    //    return View();
    //}


    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        return View();
    }


    //[HttpPost]
    //[Route("/signin")]
    //public async Task<IActionResult> SignIn()
    //{
    //    return View();
    //}


}
