namespace HowsYourDayApi.Application.DTOs.Day
{
    public class CreateDayEntryDto
    {
        public int Rating { get; set; }
        public string? Note { get; set; } = null;
    }   
}