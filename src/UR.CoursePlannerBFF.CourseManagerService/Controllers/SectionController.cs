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
    public class SectionController : ControllerBase{

        private readonly IConfiguration _configuration;
        private readonly ICourseManagerApiService _courseManagerService;

        public SectionController(IConfiguration configuration, ICourseManagerApiService courseManagerService)
        {
            _configuration = configuration;
            _courseManagerService = courseManagerService;
        }
        [HttpGet("Id({id})")]
        public IActionResult GetSectionByCourseId(int id)
        {
            List<Sections> courses = new List<Sections>();
            //have do some thing about the above line
            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();

                string sql = "GetSectionByCourseId"; // SQL PROC to get all Courses
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sections section = new Sections
                            {
                                coursesection_id = reader.IsDBNull(reader.GetOrdinal("coursesection_id")) ?
                                                        0 : Convert.ToInt32(reader["coursesection_id"]),
                                coursesection_year = reader.IsDBNull(reader.GetOrdinal("coursesection_year")) ?
                                                        0 : Convert.ToInt32(reader["coursesection_year"]),
                                coursesection_term = reader.IsDBNull(reader.GetOrdinal("coursesection_term")) ?
                                                        0 : Convert.ToInt32(reader["coursesection_term"]),
                                coursesection_number = reader.IsDBNull(reader.GetOrdinal("coursesection_number")) ?
                                                        0 : Convert.ToInt32(reader["coursesection_number"]),
                                coursesection_CRN = reader.IsDBNull(reader.GetOrdinal("coursesection_CRN")) ?
                                                        0 : Convert.ToInt32(reader["coursesection_CRN"]),
                                coursesection_maxseat = reader.IsDBNull(reader.GetOrdinal("coursesection_maxseat")) ?
                                                        0 : Convert.ToInt32(reader["coursesection_maxseat"]),
                                coursesection_maxwaitlist = reader.IsDBNull(reader.GetOrdinal("coursesection_maxwaitlist")) ?
                                                        0 : Convert.ToInt32(reader["coursesection_maxwaitlist"]),
                                labsection_id = reader.IsDBNull(reader.GetOrdinal("labsection_id")) ?
                                                        0 : Convert.ToInt32(reader["labsection_id"]),
                                coursecatalog_id = reader.IsDBNull(reader.GetOrdinal("coursecatalog_id")) ?
                                                        0 : Convert.ToInt32(reader["coursecatalog_id"])
                            };
                            courses.Add(section);
                        }
                    }
                }
            }

            return Ok(courses);
        }


        [HttpGet("Term({term})/Year({year})")]
        public IActionResult GetCourseByTermAndYear(int term, int year)
        {
            List<Sections> courses = new List<Sections>();
            //have do some thing about the above line
            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();

                string sql = "GetCourseByTermAndYear"; // SQL PROC to get all Courses
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@term", term);
                    command.Parameters.AddWithValue("@year", year);


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sections section = new Sections
                            {
                                coursesection_id = DBNull.Value.Equals(reader["coursesection_id"]) ?
                                                        0 : Convert.ToInt32(reader["coursesection_id"]),
                                coursesection_year = DBNull.Value.Equals(reader["coursesection_year"]) ?
                                                        0 : Convert.ToInt32(reader["coursesection_year"]),
                                coursesection_term = DBNull.Value.Equals(reader["coursesection_term"]) ?
                                                        0 : Convert.ToInt32(reader["coursesection_term"]),
                                coursesection_number = DBNull.Value.Equals(reader["coursesection_number"]) ?
                                                        0 : Convert.ToInt32(reader["coursesection_number"]),
                                coursesection_CRN = DBNull.Value.Equals(reader["coursesection_CRN"]) ?
                                                        0 : Convert.ToInt32(reader["coursesection_CRN"]),
                                coursesection_maxseat = DBNull.Value.Equals(reader["coursesection_maxseat"]) ?
                                                        0 : Convert.ToInt32(reader["coursesection_maxseat"]),
                                coursesection_maxwaitlist = DBNull.Value.Equals(reader["coursesection_maxwaitlist"]) ?
                                                        0 : Convert.ToInt32(reader["coursesection_maxwaitlist"]),
                                labsection_id = DBNull.Value.Equals(reader["labsection_id"]) ?
                                                        0 : Convert.ToInt32(reader["labsection_id"]),
                                coursecatalog_id = DBNull.Value.Equals(reader["coursecatalog_id"]) ?
                                                        0 : Convert.ToInt32(reader["coursecatalog_id"])
                            };
                            courses.Add(section);
                        }
                    }
                }
            }

            return Ok(courses);
        }
        [HttpGet("Requirement_id({requirementId})")]
        public IActionResult GetCourseByRequirementId(int requirementId)
        {
            List<CourseCatalog> coursesCata = new List<CourseCatalog>();
            //have do some thing about the above line
            using (SqlConnection connection = new SqlConnection(_configuration.GetSection("SQLDatabase:ConnectionStrings").Value))
            {
                connection.Open();

                string sql = "GetCourseByRequirementId"; // SQL PROC to get all Courses
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@requirementId", requirementId);



                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CourseCatalog courseCata = new CourseCatalog
                            {
                                coursecatalog_id = reader.IsDBNull(reader.GetOrdinal("coursecatalog_id")) ?
                                                        0 : Convert.ToInt32(reader["coursecatalog_id"]),
                                coursecatalog_name = reader["coursecatalog_name"].ToString(),
                                faculty_id = reader.IsDBNull(reader.GetOrdinal("faculty_id")) ?
                                                        0 : Convert.ToInt32(reader["faculty_id"]),
                                coursecatalog_number = reader.IsDBNull(reader.GetOrdinal("coursecatalog_number")) ?
                                                        0 : Convert.ToInt32(reader["coursecatalog_number"]),
                                courseattribute_id = reader.IsDBNull(reader.GetOrdinal("courseattribute_id")) ?
                                                        0 : Convert.ToInt32(reader["courseattribute_id"]),
                                coursesubject_id = reader.IsDBNull(reader.GetOrdinal("coursesubject_id")) ?
                                                        0 : Convert.ToInt32(reader["coursesubject_id"])
                            };
                            coursesCata.Add(courseCata);
                        }
                    }
                }
            }

            return Ok(coursesCata);
        }
    }
}