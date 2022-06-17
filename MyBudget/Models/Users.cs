using Microsoft.AspNetCore.Identity;

namespace MyBudget.Models
{
    public class Users:IdentityUser
    {
        public string? user_img { get; set; }
    }
}
