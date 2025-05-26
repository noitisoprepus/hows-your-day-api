using HowsYourDayApi.Application.DTOs.Account;
using HowsYourDayApi.Domain.Entities;

namespace HowsYourDayApi.Domain.Interfaces
{
    public interface ITokenService
    {
        Task<TokenDTO> CreateToken(AppUser user, bool populateExpiry);
        Task<TokenDTO> RefreshToken(TokenDTO tokenDTO);
        void StoreTokensToCookie(TokenDTO tokenDTO, HttpContext context);
        void ClearTokenCookie(HttpContext context);
    }
}