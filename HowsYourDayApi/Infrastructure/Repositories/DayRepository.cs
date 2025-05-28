using HowsYourDayApi.Domain.Entities;
using HowsYourDayApi.Domain.Interfaces;
using HowsYourDayApi.Infrastructure.Persistence;

namespace HowsYourDayApi.Infrastructure.Repositories
{
    public class DayRepository : IDayRepository
    {
        private readonly HowsYourDayAppDbContext _context;

        public DayRepository(HowsYourDayAppDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Day>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Day> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Day>> SearchAsync(Guid? userId = null, DateTime? searchDateFromUtc = null, DateTime? searchDateToUtc = null)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Day day)
        {
            _context.Days.Add(day);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Day day)
        {
            _context.Days.Update(day);

            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Day day)
        {
            _context.Days.Remove(day);

            await _context.SaveChangesAsync();
        }
    }
}
