namespace HowsYourDayApi.Domain.Entities
{
    public class DaySummary
    {
        public Guid Id { get; set; }
        public DateTime DateStampUtc { get; set; }
        public decimal AverageRating { get; set; }
    }
}