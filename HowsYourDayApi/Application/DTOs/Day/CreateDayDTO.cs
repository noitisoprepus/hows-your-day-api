using System.ComponentModel.DataAnnotations;

namespace HowsYourDayApi.Application.DTOs.Day
{
    public class CreateDayDTO
    {
        [Required]
        public int Rating { get; set; }
        public string? Note { get; set; } = null;
    }   
}