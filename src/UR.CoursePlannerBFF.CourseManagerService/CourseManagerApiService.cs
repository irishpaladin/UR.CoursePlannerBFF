using Dapper;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManagerService
{
    public interface ICourseManagerApiService
    {
        public CourseModel GetCourseById(int courseId);
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
            const string sqlCommand = "[dbo].[GetCourseDataById]";
            var sqlParameter = new
            {
                id = courseId
            };
            var result = _connection.GetConnection()
                .Query<CourseModel>( sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if(result == null ) { throw new Exception($"Course with {courseId} ID does not exist"); }
            return result.FirstOrDefault();
        }
    }
}
