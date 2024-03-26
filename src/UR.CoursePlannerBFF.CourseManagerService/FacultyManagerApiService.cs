using Dapper;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManagerService
{
    public interface IFacultyManagerApiService
    {
        Task<FacultyModel> GetFacultyById(int id);
        Task<IEnumerable<FacultyModel>> GetAllFaculty();
    }
    public class FacultyManagerApiService : IFacultyManagerApiService
    {
        private readonly IDBConnectionProvider _connection;
        public FacultyManagerApiService(IDBConnectionProvider connection) { 
            _connection = connection;
        }

        public Task<IEnumerable<FacultyModel>> GetAllFaculty()
        {
            const string sqlCommand = "[dbo].[GetAllFaculties]";
            var result = _connection.GetConnection()
                .QueryAsync<FacultyModel>(sqlCommand, commandType: CommandType.StoredProcedure);
            return result;
        }

        Task<FacultyModel> IFacultyManagerApiService.GetFacultyById(int id)
        {
            var allFaculty = GetAllFaculty().Result;
            var result = allFaculty.Where(f => f.faculty_id == id);
            return Task.FromResult(result.FirstOrDefault());
        }
    }
}
