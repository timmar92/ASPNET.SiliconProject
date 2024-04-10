using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;


    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;


    [ProtectedPersonalData]
    public string? Biography { get; set; }

    public string? ProfileImage { get; set; } = "profile-placeholder.svg";


    public bool IsExternalAccount { get; set; } = false;


    public ICollection<AddressEntity> Addresses { get; set; } = [];
}
