using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AccountService
{
    private readonly DataContext _context;
    private readonly UserManager<UserEntity> _userManager;
    private readonly AddressRepository _addressRepository;
    private readonly UserRepository _userRepository;

    public AccountService(DataContext context, UserManager<UserEntity> userManager, AddressRepository addressRepository, UserRepository userRepository)
    {
        _context = context;
        _userManager = userManager;
        _addressRepository = addressRepository;
        _userRepository = userRepository;
    }

    public async Task<UserEntity> UpdateInfoAsync(UserEntity user)
    {
        try
        {
            var result = await _userRepository.UpdateEntityAsync(x => x.Id == user.Id, user);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }
}
