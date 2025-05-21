using HowsYourDayApi.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace HowsYourDayAPI.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO, HttpContext httpContext);
        Task<SignInResult> LoginAsync(LoginDTO login, HttpContext httpContext);
        Task LogoutAsync(HttpContext httpContext);
    }
}
