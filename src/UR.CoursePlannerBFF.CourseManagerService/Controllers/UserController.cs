using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using UR.CoursePlannerBFF.CourseManagerService;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserManagerApiService _userManagerService;

        public UserController(IConfiguration configuration, IUserManagerApiService userManagerService)
        {
            _configuration = configuration;
            _userManagerService = userManagerService;
        }
        [HttpGet("Requirement/UserID({userId})")]
        public IActionResult GetRequirementsByUserId(int userId)
        {
            List<Requirement> requirements = new List<Requirement>();
            //have do some thing about the above line
            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();
                string sql = "GetRequirementsByUserId"; // SQL PROC to get all Courses
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Requirement requirement = new Requirement
                            {
                                requirement_id = Convert.ToInt32(reader["requirement_id"]),
                                requirement_occurence = Convert.ToInt32(reader["requirement_occurence"]),
                                requirement_name = reader["requirement_name"].ToString(),
                                requirement_status = Convert.ToInt32(reader["requirement_status"]),
                                audit_id = Convert.ToInt32(reader["audit_id"]),
                            };
                            requirements.Add(requirement);
                        }
                    }
                }
            }

            return Ok(requirements);
        }
        [HttpGet("Schedule/UserID({userId})")]
        public IActionResult GetSchedulesByUserId(int userId)
        {
            List<SectionSchedules> schedules = new List<SectionSchedules>();
            //have do some thing about the above line
            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();

                string sql = "GetSchedulesByUserId"; // SQL PROC to get all Courses
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SectionSchedules schedule = new SectionSchedules
                            {
                                sectionschedule_id = Convert.ToInt32(reader["sectionschedule_id"]),
                                coursesection_id = Convert.ToInt32(reader["coursesection_id"]),
                                sectionschedule_day = Convert.ToInt32(reader["sectionschedule_day"]),
                                sectionschedule_starttime = Convert.ToInt32(reader["sectionschedule_starttime"]),
                                sectionschedule_endtime = Convert.ToInt32(reader["sectionschedule_endtime"]),


                            };
                            schedules.Add(schedule);
                        }
                    }
                }
            }

            return Ok(schedules);
        }




    }
}