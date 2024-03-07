using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressManager
{
    private readonly AddressRepository _addressRepository;
    private readonly UserRepository _userRepository;

    public AddressManager(AddressRepository addressRepository, UserRepository userRepository)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
    }


    public async Task<AddressEntity> GetAddressAsync(string userId)
    {
        try
        {
            var addressEntity = await _addressRepository.GetOneAsync(x => x.UserId == userId);
            return addressEntity!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }


    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
        try
        {
            entity = await _addressRepository.CreateOneAsync(entity);
            if (entity != null)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false!;
    }


    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {
        try
        {
            var existingEntity = await _addressRepository.GetOneAsync(x => x.UserId == entity.UserId);
            if (existingEntity != null)
            {
                await _addressRepository.UpdateOneAsync(entity);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
        
    }

}
