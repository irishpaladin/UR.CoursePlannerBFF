using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
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
        [HttpGet("Requirement/UserID({account_id})")]
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
        [HttpGet("Schedule/UserID({account_id})")]
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

        [HttpGet("Filtere/UserID({account_id})")]
        public IActionResult GetUserFiltersByUserId(int userId)
        {
            List<Filter> filters = new List<Filter>();
            //have do some thing about the above line
            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();

                string sql = "GetUserFiltersByUserId"; // SQL PROC to get all Courses
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Filter filter= new Filter()
                            {
                                account_id = Convert.ToInt32(reader["account_id"]),
                                coursecatalogfilter_id = Convert.ToInt32(reader["coursecatalogfilter_id"]),
                                coursecatalog_id = Convert.ToInt32(reader["coursecatalog_id"]),
                                coursesubjectfilter_id = Convert.ToInt32(reader["coursesubjectfilter_id"]),
                                coursesubject_id = Convert.ToInt32(reader["coursesubject_id"]),
                                coursecatalog_number = Convert.ToInt32(reader["coursecatalog_number"]),
                                faculty_id = Convert.ToInt32(reader["faculty_id"]),
                                courseattribute_id = Convert.ToInt32(reader["courseattribute_id"]),
                                coursecatalog_name = reader["coursecatalog_name"].ToString(),
                            };
                            filters.Add(filter);
                        }
                    }
                }
            }

            return Ok(filters);
        }
        [HttpPost("Filtere/UserID({account_id})")]
        public IActionResult SaveUserFiltersByUserId(int userId, SaveFilters filter)
        {

            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {

                if (filter == null)
                {
                    throw new ArgumentException("No filters provided.", nameof(filter));
                }
                connection.Open();

                string sql = "SaveUserFiltersByUserId"; // SQL PROC to get all Courses
               
                    using (SqlCommand insertCommand = new SqlCommand(sql, connection))
                    {
                        insertCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        insertCommand.Parameters.AddWithValue("@accountId", filter.account_id);
                        insertCommand.Parameters.AddWithValue("@courseSubjectId", filter.coursesubject_id);
                        insertCommand.Parameters.AddWithValue("@courseCatalogId", filter.coursecatalog_id);
                        insertCommand.ExecuteNonQuery();

                }
                
            }

            return Ok(filter);
        }

        [HttpGet("User({account_id})")]
        public IActionResult GetUserInfoBySubclaim(string subclaim)
        {
            List<User> users = new List<User>();
            //have do some thing about the above line
            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();

                string sql = "GetUserInfoBySubclaim"; // SQL PROC to get all Courses
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@subclaim", subclaim);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User()
                            {
                                account_id = Convert.ToInt32(reader["account_id"]),
                                subclaim = reader["subclaim"].ToString(),
                                account_email = reader["account_email"].ToString(),
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return Ok(users);
        }


    }
}