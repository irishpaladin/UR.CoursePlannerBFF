using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UR.CoursePlannerBFF.RequirementSchedule.Models;
using static UR.CoursePlannerBFF.RequirementSchedule.RequirementScheduleApiService;

namespace UR.CoursePlannerBFF.RequirementSchedule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequirementScheduleController : ControllerBase
    {
        private readonly IRequirementSchedulerApiService requirementSchedulerService;

        public RequirementScheduleController(IRequirementSchedulerApiService courseManagerService)
        {
            requirementSchedulerService = courseManagerService;
        }

        [HttpGet]
        public IActionResult GetRequirementSchedulesByUserId(int userId)
        {
            IEnumerable<RequirementScheduleModel> result;
            try
            {
                result = requirementSchedulerService.GetRequirementSchedulesByUserId(userId);
                if (result == null || !result.Any())
                    return NotFound("No courses found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(result);
        }
    }
}
