using HowsYourDayApi.Application.DTOs.Day;
using HowsYourDayApi.Domain.Entities;
using HowsYourDayApi.Domain.Interfaces;
using HowsYourDayApi.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HowsYourDayApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DayController : ControllerBase
    {
        private readonly IDayEntryService _dayService;
        private readonly IDaySummaryService _daySummaryService;

        public DayController(IDayEntryService dayService, IDaySummaryService daySummaryService)
        {
            _dayService = dayService;
            _daySummaryService = daySummaryService;
        }

        #region Day Entry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayEntry>>> GetDays()
        {
            var days = await _dayService.GetDayEntriesAsync();
            
            return Ok(days);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DayEntry>> GetDay(Guid id)
        {
            var day = await _dayService.GetDayEntryAsync(id);
            if (day == null)
                return NotFound();
            
            return Ok(day);
        }

        [HttpGet("me/status")]
        public async Task<ActionResult<bool>> HasUserPostedToday()
        {
            var userId = ClaimsPrincipalExtensions.GetUserId(User);

            var hasPostedToday = await _dayService.HasUserPostedTodayAsync(userId);

            return Ok(hasPostedToday);
        }

        [HttpGet("me/today")]
        public async Task<ActionResult<DayEntryDto>> GetDayEntryOfUserToday()
        {
            var userId = ClaimsPrincipalExtensions.GetUserId(User);

            var dayToday = await _dayService.GetDayEntryOfUserTodayAsync(userId);

            return Ok(dayToday);
        }

        [HttpGet("me/day")]
        public async Task<ActionResult<IEnumerable<DayEntryDto>>> GetDaysOfUser()
        {
            var userId = ClaimsPrincipalExtensions.GetUserId(User);

            var days = await _dayService.GetDaysEntriesOfUserAsync(userId);

            return Ok(days);
        }

        [HttpPost("me/day")]
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

            return CreatedAtAction(nameof(GetDayEntryOfUserToday), null);
        }

        [HttpPut("me/day")]
        public async Task<ActionResult<DayEntryDto>> EditDayOfUserToday([FromBody] CreateDayEntryDto day)
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

            return CreatedAtAction(nameof(GetDayEntryOfUserToday), null);
        }
        #endregion
    }
}