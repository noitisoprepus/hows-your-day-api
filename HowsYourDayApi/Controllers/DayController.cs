using System.Security.Claims;
using HowsYourDayApi.DTOs.Day;
using HowsYourDayAPI.Interfaces;
using HowsYourDayAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HowsYourDayAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class DayController : ControllerBase
    {
        private readonly IDayService _dayService;

        public DayController(IDayService dayService)
        {
            _dayService = dayService;
        }

        [HttpGet("day")]
        public async Task<ActionResult<IEnumerable<Day>>> GetDays()
        {
            var days = await _dayService.GetDaysAsync();
            return Ok(days);
        }

        [HttpGet("day/{id}")]
        public async Task<ActionResult<Day>> GetDay(Guid id)
        {
            var day = await _dayService.GetDayAsync(id);
            if (day == null) return NotFound();
            
            return Ok(day);
        }

        [HttpGet("day/average")]
        public async Task<ActionResult<int>> GetAverageRating()
        {
            var average = await _dayService.GetAverageRatingAsync();
            return Ok(average);
        }

        [HttpGet("account/day/status")]
        public async Task<ActionResult<bool>> HasUserPostedToday()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasPostedToday = await _dayService.HasUserPostedTodayAsync(userId);
            return Ok(hasPostedToday);
        }

        [HttpGet("account/day/today")]
        public async Task<ActionResult<CreateDayDTO>> GetUserDayToday()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dayToday = await _dayService.GetUserDayTodayAsync(userId);
            return Ok(dayToday);
        }

        [HttpGet("account/day")]
        public async Task<ActionResult<IEnumerable<Day>>> GetDaysForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var days = await _dayService.GetDaysForUserAsync(userId);
            return Ok(days);
        }

        [HttpPost("account/day")]
        public async Task<ActionResult<Day>> PostDayForUser([FromBody] CreateDayDTO day)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var createdDay = await _dayService.AddDayForUserAsync(userId, day);
            if (createdDay == null) return BadRequest("You have already posted today.");
            
            return CreatedAtAction(nameof(GetDaysForUser), new { userId }, createdDay);
        }
    }
}