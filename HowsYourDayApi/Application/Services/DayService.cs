using HowsYourDayApi.Application.DTOs.Day;
using HowsYourDayApi.Domain.Entities;
using HowsYourDayApi.Domain.Interfaces;

namespace HowsYourDayApi.Application.Services
{
    public class DayService: IDayService
    {
        private readonly IDayRepository _dayRepository;

        public DayService(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        public async Task<IEnumerable<Day>> GetDaysAsync()
        {
            return await _dayRepository.GetAllAsync();
        }

        public async Task<Day?> GetDayAsync(Guid dayId)
        {
            return await _dayRepository.GetByIdAsync(dayId);
        }

        public async Task<int> GetAverageRatingAsync()
        {
            var entries = await _dayRepository.SearchAsync(searchDateFromUtc: DateTime.UtcNow.Date);
            
            double sum = entries.Sum(entry => entry.Rating);
            double average = sum / entries.Count();

            return (int)Math.Round(average);
        }

        public async Task<bool> HasUserPostedTodayAsync(Guid userId)
        {
            var entryToday = (await _dayRepository.SearchAsync(userId, DateTime.UtcNow)).SingleOrDefault();

            return entryToday != null;
        }

        public async Task<CreateDayDTO> GetUserDayTodayAsync(Guid userId)
        {
            var entryToday = (await _dayRepository.SearchAsync(userId, DateTime.UtcNow)).SingleOrDefault();

            var entryTodayDto = new CreateDayDTO();

            if (entryToday != null)
            {
                entryTodayDto.Rating = entryToday.Rating;
                entryTodayDto.Note = entryToday.Note;
            }

            return entryTodayDto;
        }

        public async Task<IEnumerable<Day>> GetDaysForUserAsync(Guid userId)
        {
            // Retrieve all days for a specific user
            return await _dayRepository.SearchAsync(userId);
        }

        public async Task AddDayForUserAsync(Guid userId, CreateDayDTO createDayDto)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));
            if (createDayDto == null)
                throw new ArgumentNullException(nameof(createDayDto), "Day cannot be null.");

            var hasPostedToday = await HasUserPostedTodayAsync(userId);

            if (hasPostedToday)
                throw new InvalidOperationException("User has already posted today.");

            var newDayEntry = new Day
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                LogDateUtc = DateTime.UtcNow,
                Rating = createDayDto.Rating,
                Note = createDayDto.Note
            };

            await _dayRepository.InsertAsync(newDayEntry);
        }
    }
}
