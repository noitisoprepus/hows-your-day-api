namespace HowsYourDayApi.Domain.Entities
{
    public class Day
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime LogDateUtc { get; set; }
        public int Rating { get; set; }
        public string? Note { get; set; }
    }
}