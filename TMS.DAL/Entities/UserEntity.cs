using Microsoft.AspNetCore.Identity;

namespace TMS.DAL.Entities;

public class UserEntity : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
