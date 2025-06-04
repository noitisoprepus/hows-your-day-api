using HowsYourDayApi.Application.DTOs.Day;
using HowsYourDayApi.Domain.Entities;
using HowsYourDayApi.Domain.Interfaces;
using HowsYourDayApi.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HowsYourDayApi.API.Controllers
{
    [ApiController]
    [Authorize]
    public class DayEntryController : ControllerBase
    {
        private readonly IDayEntryService _dayService;

        public DayEntryController(IDayEntryService dayService)
        {
            _dayService = dayService;
        }

        [HttpGet("day")]
        public async Task<ActionResult<IEnumerable<DayEntry>>> GetDays()
        {
            var days = await _dayService.GetDayEntriesAsync();
            
            return Ok(days);
        }

        [HttpGet("day/{id}")]
        public async Task<ActionResult<DayEntry>> GetDay(Guid id)
        {
            var day = await _dayService.GetDayEntryAsync(id);
            if (day == null)
                return NotFound();
            
            return Ok(day);
        }

        [HttpGet("account/day/status")]
        public async Task<ActionResult<bool>> HasUserPostedToday()
        {
            var userId = ClaimsPrincipalExtensions.GetUserId(User);

            var hasPostedToday = await _dayService.HasUserPostedTodayAsync(userId);

            return Ok(hasPostedToday);
        }

        [HttpGet("account/day/today")]
        public async Task<ActionResult<CreateDayEntryDto>> GetDayEntryOfUserToday()
        {
            var userId = ClaimsPrincipalExtensions.GetUserId(User);

            var dayToday = await _dayService.GetDayEntryOfUserTodayAsync(userId);

            return Ok(dayToday);
        }

        [HttpGet("account/day")]
        public async Task<ActionResult<IEnumerable<DayEntry>>> GetDaysOfUser()
        {
            var userId = ClaimsPrincipalExtensions.GetUserId(User);

            var days = await _dayService.GetDaysEntriesOfUserAsync(userId);

            return Ok(days);
        }

        [HttpPost("account/day")]
        public async Task<ActionResult<DayEntry>> CreateDayOfUser([FromBody] CreateDayEntryDto day)
        {
            var userId = ClaimsPrincipalExtensions.GetUserId(User);

            try
            {
                await _dayService.InsertDayEntryOfUserAsync(userId, day);
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occurred while posting your day: {ex.Message}");
            }

            return Created();
            //return CreatedAtAction(nameof(GetUserDayToday), new { userId });
        }

        [HttpPut("account/day")]
        public async Task<ActionResult<DayEntry>> EditDayOfUserToday([FromBody] CreateDayEntryDto day)
        {
            var userId = ClaimsPrincipalExtensions.GetUserId(User);

            try
            {
                await _dayService.UpdateDayEntryOfUserTodayAsync(userId, day);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while updating your day: {ex.Message}");
            }

            return Created();
            //return CreatedAtAction(nameof(GetUserDayToday), new { userId });
        }
    }
}