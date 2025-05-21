using Microsoft.AspNetCore.Identity;

namespace HowsYourDayAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}