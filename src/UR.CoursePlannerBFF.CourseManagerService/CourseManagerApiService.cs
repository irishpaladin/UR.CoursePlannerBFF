using Dapper;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManagerService
{
    public interface ICourseManagerApiService
    {
        public CourseModel GetCourseById(int courseId);
        public IEnumerable<CourseModel> GetAllCourses();
        public CourseModel GetCourseByName(string subject, int number);
      
    }
    public class CourseManagerApiService: ICourseManagerApiService
    {
        private readonly IDBConnectionProvider _connection;

        public CourseManagerApiService(IDBConnectionProvider connection )
        {
            _connection = connection;
        }

        public CourseModel GetCourseById(int courseId)
        {
            var allCourses = GetAllCourses();
            var result = allCourses.Where(course => course.coursecatalog_id == courseId);

            if (result == null ) { throw new Exception($"Course with {courseId} ID does not exist"); }
            return result.FirstOrDefault();
        }

        public IEnumerable<CourseModel> GetAllCourses()
        {
            const string sqlCommand = "[dbo].[CourseDetails]";
           
            var result = _connection.GetConnection()
                .Query<CourseModel>( sqlCommand,  commandType: CommandType.StoredProcedure);

            if(result == null ) { throw new Exception($"No Course List found"); }
            return result;
        }

        public CourseModel GetCourseByName(string subject, int number)
        {
            var allCourses = GetAllCourses();
            var result = allCourses.Where(course => course.coursesubject == subject && course.coursecatalog_number == number);

            if (result == null || !result.Any())
            {
                throw new Exception($"Course with subject '{subject}' and number '{number}' does not exist");
            }

            return result.FirstOrDefault();
        }

    }
}
