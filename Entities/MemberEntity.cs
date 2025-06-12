using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class MemberEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string? FullName { get; set; }

    [ProtectedPersonalData]
    public string? JobTitle { get; set; }

    public MemberAddressEntity? Address { get; set; }
}

