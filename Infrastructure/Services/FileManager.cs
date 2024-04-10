
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

namespace Infrastructure.Services;

public class FileManager
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IConfiguration _configuration;
    private readonly DataContext _dataContext;

    public FileManager(UserManager<UserEntity> userManager, IConfiguration configuration, DataContext dataContext)
    {
        _userManager = userManager;
        _configuration = configuration;
        _dataContext = dataContext;
    }

    public async Task<bool> UploadProfileImage (ClaimsPrincipal user, IFormFile file)
    {
        try
        {
            if (user != null && file != null && file.Length != 0)
            {
                var userEntity = await _userManager.GetUserAsync(user);
                if (userEntity != null)
                {
                    var fileName = $"p_{userEntity.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUploadPath"]!, fileName);
                    //Path.combine() måste ha befintliga kataloger


                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    userEntity.ProfileImage = fileName;
                    _dataContext.Update(userEntity);
                    await _dataContext.SaveChangesAsync();

                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
}
