namespace HowsYourDayApi.Domain.Interfaces
{
    public interface IDaySummaryService
    {
        Task<int> GetAverageRatingAsync();
    }
}
