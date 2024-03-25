using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManagerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyManagerApiService _facultyManagerApiService;
        public FacultyController(IFacultyManagerApiService facultyManagerApiService) { 
            _facultyManagerApiService = facultyManagerApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            IEnumerable<FacultyModel> result;
            try
            {
                result = _facultyManagerApiService.GetAllFaculty().Result;
                if (result == null || !result.Any())
                    return NotFound("No faculty found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(result);
        }
        [HttpGet("Id({facultyId})")]
        public async Task<IActionResult> GetCourses(int facultyId)
        {
            FacultyModel result;
            try
            {
                result = _facultyManagerApiService.GetFacultyById(facultyId).Result;
                if (result == null)
                    return NotFound("No faculty found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(result);
        }
    }
}
