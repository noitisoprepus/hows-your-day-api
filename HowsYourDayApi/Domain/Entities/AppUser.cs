using Microsoft.AspNetCore.Identity;

namespace HowsYourDayApi.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedOnUtc { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}