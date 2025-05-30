using HowsYourDayApi.Domain.Entities;

namespace HowsYourDayApi.Domain.Interfaces
{
    public interface IDaySummaryRepository
    {
        Task<IEnumerable<DaySummary>> GetAllAsync();
        Task<DaySummary> GetByIdAsync(Guid id);
        Task<DaySummary> GetByDateAsync(DateTime date);
        Task<IEnumerable<DaySummary>> SearchAsync(DateTime searchDateFromUtc, DateTime searchDateToUtc);
        Task InsertAsync(DaySummary summary);
        Task UpdateAsync(DaySummary summary);
        Task DeleteAsync(DaySummary summary);
    }
}
