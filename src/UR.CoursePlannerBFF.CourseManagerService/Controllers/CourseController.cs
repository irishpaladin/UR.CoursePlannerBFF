using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using UR.CoursePlannerBFF.CourseManagerService;
using UR.CoursePlannerBFF.CourseManagerService.Models;

/*  example:
    for whole course list (/course) :           http://localhost7173/Course 
    for course by id (course/id/1) :            http://localhost7173/Course/id/1 
    for course by name (course/name/CS 476) :   http://localhost7173/Course/name/CS%20476 
    (%20 for space)
*/

// TODO: Update to use the singleton DBConnectionProvider to remove SQLClient packges on this proj
namespace UR.CoursePlannerBFF.CourseManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICourseManagerApiService _courseManagerService;

        public CourseController(IConfiguration configuration, ICourseManagerApiService courseManagerService)
        {
            _configuration = configuration;
            _courseManagerService = courseManagerService;
        }

       [HttpGet("All")]
        public IActionResult GetCourses()
        {       
            IEnumerable<CourseModel> result;
            try
                {
                    result = _courseManagerService.GetAllCourses();
                    if (result == null || !result.Any())
                    return NotFound("No courses found");
                }
            catch (Exception ex)
                {
                   return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            return Ok(result);
        }
        

        [HttpGet("Id({id})")]
        public IActionResult GetCourseById(int id)
        {
            CourseModel result;
            try
            {
                result = _courseManagerService.GetCourseById(id);
                if (result == null)
                    return NotFound($"Course with course id ({id}) is not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(result);
        }



        [HttpGet("Name({name})")]
        public IActionResult GetCourseByName(string name)
        {          
            string[] parts = name?.Split(' ');
            if (parts.Length != 2 || !int.TryParse(parts[1], out int number))
            {
                return BadRequest("Invalid course name format, please input with a space, eg- CTCH 311");
            }

            try
            {
                var course = _courseManagerService.GetCourseByName(parts[0], number);
                if (course == null)
                {
                    return NotFound("Course not found");
                }
                return Ok(course);
            }
            catch (Exception ex)
            {   
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        } 
    }


}
