using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UR.CoursePlannerBFF.CourseManagerService;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagerApiService _userManagerService;

        public UserController(IConfiguration configuration, IUserManagerApiService userManagerService)
        {
            _userManagerService = userManagerService;
        }
        [HttpGet("Requirement/UserID({userId})")]
        public IActionResult GetRequirementsByUserId(int userId)
        {
            IEnumerable<Requirement> requirements;
            try
            {
                requirements = _userManagerService.GetRequirementsByUserId(userId);
                if (requirements == null || !requirements.Any())
                    return NotFound("No courses found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(requirements);
        }
        [HttpGet("Schedule/UserID({userId})")]
        public IActionResult GetSchedulesByUserId(int userId)
        {
            //have do some thing about the above line
            IEnumerable<SectionSchedules> schedules;
            try
            {
                schedules = _userManagerService.GetSchedulesByUserId(userId);
                if (schedules == null || !schedules.Any())
                    return NotFound("No courses found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(schedules);
        }




    }
}