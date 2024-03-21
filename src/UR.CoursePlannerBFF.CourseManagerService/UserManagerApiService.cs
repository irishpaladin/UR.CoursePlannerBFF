using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManagerService
{
    public interface IUserManagerApiService
    {
        public Requirement GetRequirementsByUserId(int userId);
        public SectionSchedules GetSchedulesByUserId(int userId);

    }
    public class UserManagerApiService : IUserManagerApiService
    {
        private readonly IDBConnectionProvider _connection;

        public UserManagerApiService(IDBConnectionProvider connection)
        {
            _connection = connection;
        }

        public Requirement GetRequirementsByUserId(int userId)
        {
            const string sqlCommand = "[dbo].[GetRequirementsByUserId]";
            var sqlParameter = new
            {
                id = userId
            };
            var result = _connection.GetConnection()
                .Query<Requirement>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null) { throw new Exception($"Course with {userId} ID does not exist"); }
            return result.FirstOrDefault();
        }
        public SectionSchedules GetSchedulesByUserId(int userId)
        {
            const string sqlCommand = "[dbo].[GetSchedulesByUserId]";
            var sqlParameter = new
            {
                id = userId
            };
            var result = _connection.GetConnection()
                .Query<SectionSchedules>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null) { throw new Exception($"Course with {userId} ID does not exist"); }
            return result.FirstOrDefault();

        }


    }
}