using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HowsYourDayAPI.Models
{
    public class Day
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Guid DayId { get; set; }
        public string? UserId { get; set; }
        public DateTime LogDate { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}