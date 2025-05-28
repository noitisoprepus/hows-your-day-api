using HowsYourDayApi.Domain.Entities;

namespace HowsYourDayApi.Domain.Interfaces
{
    public interface IDayRepository
    {
        Task<IEnumerable<Day>> GetAllAsync();
        Task<Day> GetByIdAsync(Guid id);
        Task<IEnumerable<Day>> SearchAsync(
            Guid? userId = null, 
            DateTime? searchDateFromUtc = null, DateTime? searchDateToUtc = null);
        Task InsertAsync(Day day);
        Task UpdateAsync(Day day);
        Task DeleteAsync(Day day);
    }
}
