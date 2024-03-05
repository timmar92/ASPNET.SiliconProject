﻿using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Helpers.Middlewares;

public class UserSessionValidation(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private static bool IsAjaxRequest(HttpRequest request)
    {
        return request.Headers.XRequestedWith == "XMLHttpRequest";
    }

    public async Task InvokeAsync(HttpContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        if (context.User.Identity!.IsAuthenticated)
        {
            var user = await userManager.GetUserAsync(context.User);
            if (user == null) 
            {
                await signInManager.SignOutAsync();

                if (!IsAjaxRequest(context.Request) && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                {
                    var signInPath = "/signin";
                    context.Response.Redirect(signInPath);
                    return;
                }
            }
        }

        await _next(context);
    }


}
