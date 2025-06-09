using HowsYourDayApi.Application.DTOs.Day;

namespace HowsYourDayApi.Domain.Interfaces
{
    public interface IDaySummaryService
    {
        Task<DaySummaryDto?> GetDaySummaryOfDateAsync(DateTime dateUtc);
        Task<IEnumerable<DaySummaryDto>> GetDaySummariesAsync(DateTime fromUtc, DateTime toUtc);
        Task InsertDaySummaryAsync(DaySummaryDto daySummary);
        Task UpdateDaySummaryAsync(DaySummaryDto daySummary);
    }
}
