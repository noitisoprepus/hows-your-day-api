using HowsYourDayApi.DTOs.Day;
using HowsYourDayAPI.Data;
using HowsYourDayAPI.Interfaces;
using HowsYourDayAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HowsYourDayAPI.Services
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
            var postsToday = await _context.Days.Where(d => d.LogDate.Date == DateTime.UtcNow.Date).ToListAsync();
            
            double sum = 0.0;
            foreach(Day post in postsToday)
                sum += post.Rating;
            double average = sum / postsToday.Count;

            return (int)Math.Round(average);
        }

        public async Task<bool> HasUserPostedTodayAsync(string userId)
        {
            return await _context.Days.AnyAsync(d => d.UserId == userId &&
                d.LogDate.Date == DateTime.UtcNow.Date);
        }

        public async Task<CreateDayDTO> GetUserDayTodayAsync(string userId)
        {
            var today = DateTime.UtcNow.Date;
            var day = await _context.Days.FirstOrDefaultAsync(d => d.UserId == userId && d.LogDate.Date == today);
            if (day == null)
            {
                return new CreateDayDTO{
                    Rating = 0,
                };
            }

            return new CreateDayDTO{
                Rating = day.Rating,
                Comment = day.Comment
            };
        }

        public async Task<IEnumerable<Day>> GetDaysForUserAsync(string userId)
        {
            return await _context.Days.Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task<Day?> AddDayForUserAsync(string userId, CreateDayDTO day)
        {
            var today = DateTime.UtcNow.Date;
            var hasPostedToday = await _context.Days
                .AnyAsync(d => d.UserId == userId && d.LogDate.Date == today);
            if (hasPostedToday) return null;

            var newDay = new Day{
                DayId = Guid.NewGuid(),
                UserId = userId,
                LogDate = DateTime.UtcNow,
                Rating = day.Rating,
                Comment = day.Comment
            };
            _context.Days.Add(newDay);
            await _context.SaveChangesAsync();
            
            return newDay;
        }
    }
}
