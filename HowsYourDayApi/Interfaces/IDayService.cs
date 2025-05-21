using HowsYourDayApi.DTOs.Day;
using HowsYourDayAPI.Models;

namespace HowsYourDayAPI.Interfaces
{
    public interface IDayService
    {
        Task<IEnumerable<Day>> GetDaysAsync();
        Task<Day?> GetDayAsync(Guid dayId);
        Task<int> GetAverageRatingAsync();
        Task<bool> HasUserPostedTodayAsync(string userId);
        Task<CreateDayDTO> GetUserDayTodayAsync(string userId);
        Task<IEnumerable<Day>> GetDaysForUserAsync(string userId);
        Task<Day?> AddDayForUserAsync(string userId, CreateDayDTO day);
    }
}
