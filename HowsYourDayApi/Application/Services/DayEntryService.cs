using HowsYourDayApi.Application.DTOs.Day;
using HowsYourDayApi.Domain.Entities;
using HowsYourDayApi.Domain.Interfaces;

namespace HowsYourDayApi.Application.Services
{
    public class DayEntryService: IDayEntryService
    {
        private readonly IDayEntryRepository _dayRepository;

        public DayEntryService(IDayEntryRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        public async Task<IEnumerable<DayEntryDto>> GetDayEntriesAsync()
        {
            // Retrieve all day entries
            var dayEntries = await _dayRepository.GetAllAsync();

            // Map the day entries to DayEntryDto
            var dayEntriesDto = dayEntries.Select(entry => new DayEntryDto
            {
                LogDate = entry.LogDateUtc.ToLocalTime(),
                Rating = entry.Rating,
                Note = entry.Note
            });

            return dayEntriesDto;
        }

        public async Task<DayEntryDto?> GetDayEntryAsync(Guid dayEntryId)
        {
            var dayEntry = await _dayRepository.GetByIdAsync(dayEntryId);

            var entryDto = new DayEntryDto();

            if (dayEntry != null)
            {
                // Map the entry to DayEntryDto
                entryDto.LogDate = dayEntry.LogDateUtc.ToLocalTime();
                entryDto.Rating = dayEntry.Rating;
                entryDto.Note = dayEntry.Note;
            }

            return entryDto;
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

        public async Task<IEnumerable<DayEntryDto>> GetDaysEntriesOfUserAsync(Guid userId)
        {
            // Retrieve all days for a specific user
            var dayEntries = await _dayRepository.SearchAsync(userId);

            // Map the day entries to DayEntryDto
            var dayEntriesDto = dayEntries.Select(entry => new DayEntryDto
            {
                LogDate = entry.LogDateUtc.ToLocalTime(),
                Rating = entry.Rating,
                Note = entry.Note
            });

            return dayEntriesDto;
        }

        public async Task<DayEntryDto> GetDayEntryOfUserTodayAsync(Guid userId)
        {
            var entryToday = (await _dayRepository.SearchAsync(userId, DateTime.UtcNow)).SingleOrDefault();

            var entryTodayDto = new DayEntryDto();

            if (entryToday != null)
            {
                // Map the entry to DayEntryDto
                entryTodayDto.LogDate = entryToday.LogDateUtc.ToLocalTime();
                entryTodayDto.Rating = entryToday.Rating;
                entryTodayDto.Note = entryToday.Note;
            }

            return entryTodayDto;
        }

        public async Task AddDayEntryOfUserAsync(Guid userId, CreateDayEntryDto createDayDto)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));
            if (createDayDto == null)
                throw new ArgumentNullException(nameof(createDayDto), "Day cannot be null.");

            var hasPostedToday = await HasUserPostedTodayAsync(userId);

            if (hasPostedToday)
                throw new InvalidOperationException("User has already posted today.");

            var newDayEntry = new DayEntry
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
