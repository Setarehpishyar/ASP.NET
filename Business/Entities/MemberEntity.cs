using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
namespace Business.Entities;

public  class MemberEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string? FullName { get; set; }

    [ProtectedPersonalData]
    public string? Email { get; set; }

    [ProtectedPersonalData]
    public string? JobTitle { get; set; }

}

public class MemberAddressEntity
{
    [Key]
    public string UserId { get; set; } = null!;

    public string StreetName { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string City { get; set; } = null!;

    public MemberEntity Member { get; set; } = null!;


}

