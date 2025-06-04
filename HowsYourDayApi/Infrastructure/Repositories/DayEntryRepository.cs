using HowsYourDayApi.Domain.Entities;
using HowsYourDayApi.Domain.Interfaces;
using HowsYourDayApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HowsYourDayApi.Infrastructure.Repositories
{
    public class DayEntryRepository : IDayEntryRepository
    {
        private readonly HowsYourDayAppDbContext _context;

        public DayEntryRepository(HowsYourDayAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DayEntry>> GetAllAsync()
        {
            return await _context.DayEntries.ToListAsync();
        }

        public async Task<DayEntry> GetByIdAsync(Guid id)
        {
            return await _context.DayEntries
                .FirstOrDefaultAsync(day => day.Id == id)
                ?? throw new KeyNotFoundException($"Day entry with ID {id} not found.");
        }

        public async Task<IEnumerable<DayEntry>> SearchAsync(Guid userId, DateTime? from = null, DateTime? to = null)
        {
            return await _context.DayEntries
                .Where(day => day.UserId == userId &&
                              (!from.HasValue || day.LogDateUtc >= from) &&
                              (!to.HasValue || day.LogDateUtc <= to))
                .ToListAsync();
        }

        public async Task InsertAsync(DayEntry day)
        {
            _context.DayEntries.Add(day);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DayEntry day)
        {
            _context.DayEntries.Update(day);

            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(DayEntry day)
        {
            _context.DayEntries.Remove(day);

            await _context.SaveChangesAsync();
        }
    }
}
