using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using UR.CoursePlannerBFF.Models;

/*  example:
    for whole course list (/course) :           http://localhost7173/Course 
    for course by id (course/id/1) :            http://localhost7173/Course/id/1 
    for course by name (course/name/CS 476) :   http://localhost7173/Course/name/CS%20476 
    (%20 for space)
*/ 

namespace UR.CoursePlannerBFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CourseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
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

        [HttpGet("id/{id}")]
        public IActionResult GetCourseById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string sql = "CourseById"; // SQL PROC to get courses by id
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure; 
                    command.Parameters.AddWithValue("@course_id", id);

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

        [HttpGet("Name/{name}")]
        public IActionResult GetCourseByName(string name)
        {
            string[]? parts = name?.Split(' ');
            if ( parts.Length != 2 || !int.TryParse(parts[1], out int number))
            {
                return BadRequest("Invalid course name format");
            }

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
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
