using HowsYourDayApi.Application.DTOs.Day;
using HowsYourDayApi.Domain.Entities;

namespace HowsYourDayApi.Domain.Interfaces
{
    public interface IDayService
    {
        Task<IEnumerable<Day>> GetDaysAsync();
        Task<Day?> GetDayAsync(Guid dayId);
        Task<int> GetAverageRatingAsync();
        Task<bool> HasUserPostedTodayAsync(Guid userId);
        Task<CreateDayDTO> GetUserDayTodayAsync(Guid userId);
        Task<IEnumerable<Day>> GetDaysForUserAsync(Guid userId);
        Task AddDayForUserAsync(Guid userId, CreateDayDTO day);
    }
}
