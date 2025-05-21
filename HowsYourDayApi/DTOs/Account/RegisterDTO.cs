using System.ComponentModel.DataAnnotations;

namespace HowsYourDayApi.DTOs.Account
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }
}