using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UR.CoursePlannerBFF.RequirementSchedule.Models;
using UR.CoursePlannerBFF.RequirementSchedule;

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

        [HttpPost]
        public async Task<ActionResult<RequirementScheduleModel>> SaveRequirementSchedule([FromBody]RequirementScheduleModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                return await SaveUpdateRequirementSchedule(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private async Task<ActionResult<RequirementScheduleModel>> SaveUpdateRequirementSchedule(RequirementScheduleModel model)
        {
            //Save new Requirement Schedule
            if (model.requirementsschedules_id == null)
            {
                var createdRequirementSchedule = await requirementSchedulerService.SaveRequirementSchedule(model);
                return Created($"{Request.Path.Value}?requirementScheduleId={createdRequirementSchedule}", createdRequirementSchedule);
            }
            // Update existing schedule
            else
            {
                await requirementSchedulerService.UpdateRequirementSchedule(model.requirementsschedules_id, model.course_section_id);
                return Ok("Update successful");
            }
        }
    }
}
