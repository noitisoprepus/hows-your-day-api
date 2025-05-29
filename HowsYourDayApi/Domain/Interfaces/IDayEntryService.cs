using HowsYourDayApi.Application.DTOs.Day;

namespace HowsYourDayApi.Domain.Interfaces
{
    public interface IDayEntryService
    {
        Task<IEnumerable<DayEntryDto>> GetDayEntriesAsync();
        Task<DayEntryDto?> GetDayEntryAsync(Guid dayEntryId);
        Task<int> GetAverageRatingAsync();
        Task<bool> HasUserPostedTodayAsync(Guid userId);
        Task<IEnumerable<DayEntryDto>> GetDaysEntriesOfUserAsync(Guid userId);
        Task<DayEntryDto> GetDayEntryOfUserTodayAsync(Guid userId);
        Task AddDayEntryOfUserAsync(Guid userId, CreateDayEntryDto day);
    }
}
