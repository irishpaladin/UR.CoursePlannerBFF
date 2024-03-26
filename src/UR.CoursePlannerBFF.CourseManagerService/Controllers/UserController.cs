using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using UR.CoursePlannerBFF.CourseManagerService;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagerApiService _userManagerService;
        private readonly IConfiguration _configuration;
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
        
        //input with quotation marks . eg- "test@gmail.com"
        [HttpPost("UserIdByEmail")]
        public IActionResult GetUserIdByEmail([FromBody] User model)
        {
            string email = model.account_email;
            string subclaim = model.subclaim;

            int userId;
            try
            {                        
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(email))
                {
                    return BadRequest("Invalid email format.");
                }               

                 //to check if subclaim is empty/inputted as default string 
                if (string.IsNullOrEmpty(subclaim) || subclaim == "string")
                {
                    subclaim = null;
                }

                userId = _userManagerService.GetUserIdByEmail(email, subclaim);
                var responseObject = new
                {
                    UserId = userId
                };      
                
                return Ok(responseObject);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
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