using HowsYourDayApi.Application.DTOs.Day;
using HowsYourDayApi.Domain.Entities;
using HowsYourDayApi.Domain.Interfaces;
using HowsYourDayApi.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HowsYourDayApi.Application.Services
{
    public class DayService: IDayService
    {
        private readonly HowsYourDayAppDbContext _context;

        public DayService(HowsYourDayAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Day>> GetDaysAsync()
        {
            return await _context.Days.ToListAsync();
        }

        public async Task<Day?> GetDayAsync(Guid dayId)
        {
            return await _context.Days.FindAsync(dayId);
        }

        public async Task<int> GetAverageRatingAsync()
        {
            var postsToday = await _context.Days.Where(d => d.LogDateUtc == DateTime.UtcNow.Date).ToListAsync();
            
            double sum = 0.0;
            foreach(Day post in postsToday)
                sum += post.Rating;
            double average = sum / postsToday.Count;

            return (int)Math.Round(average);
        }

        public async Task<bool> HasUserPostedTodayAsync(Guid userId)
        {
            return await _context.Days.AnyAsync(d => d.UserId == userId &&
                d.LogDateUtc.Date == DateTime.UtcNow.Date);
        }

        public async Task<CreateDayDTO> GetUserDayTodayAsync(Guid userId)
        {
            var today = DateTime.UtcNow.Date;
            var day = await _context.Days.FirstOrDefaultAsync(d => d.UserId == userId && d.LogDateUtc.Date == today);
            if (day == null)
            {
                return new CreateDayDTO{
                    Rating = 0,
                };
            }

            return new CreateDayDTO{
                Rating = day.Rating,
                Note = day.Note
            };
        }

        public async Task<IEnumerable<Day>> GetDaysForUserAsync(Guid userId)
        {
            return await _context.Days.Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task<Day?> AddDayForUserAsync(Guid userId, CreateDayDTO day)
        {
            var today = DateTime.UtcNow.Date;
            var hasPostedToday = await _context.Days
                .AnyAsync(d => d.UserId == userId && d.LogDateUtc.Date == today);
            if (hasPostedToday) return null;

            var newDay = new Day{
                Id = Guid.NewGuid(),
                UserId = userId,
                LogDateUtc = DateTime.UtcNow,
                Rating = day.Rating,
                Note = day.Note
            };
            _context.Days.Add(newDay);
            await _context.SaveChangesAsync();
            
            return newDay;
        }
    }
}
