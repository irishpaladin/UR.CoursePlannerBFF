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

        [HttpGet]
        public IActionResult GetCourses()
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();

                string sql = "CourseDetails"; // SQL PROC to get all Courses
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure; 

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Course course = new Course
                            {
                                course_id = Convert.ToInt32(reader["course_id"]),
                                subject = reader["subject"].ToString(),
                                course_number = Convert.ToInt32(reader["course_number"])
                            };
                            courses.Add(course);
                        }
                    }
                }
            }

            return Ok(courses);
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
            string[]? parts = name?.Split(' ');
            if ( parts.Length != 2 || !int.TryParse(parts[1], out int number))
            {
                return BadRequest("Invalid course name format");
            }

            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();

                string sql = "CourseByName"; // SQL Proc to get courses by Name : subject + course_number
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure; 
                    command.Parameters.AddWithValue("@subject", parts[0]);
                    command.Parameters.AddWithValue("@number", number);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Course course = new Course
                            {
                                course_id = Convert.ToInt32(reader["course_id"]),
                                subject = reader["subject"].ToString(),
                                course_number = Convert.ToInt32(reader["course_number"])
                            };
                            return Ok(course);
                        }
                        else
                        {
                            return NotFound("Course not found");
                        }
                    }
                }
            }
        }
    }


}
