using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class MemberAddressEntity
{
    [Key]
    public string MemberId { get; set; } = null!;

    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    [ForeignKey(nameof(MemberId))]
    public MemberEntity Member { get; set; } = null!;
}

