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
        public int GetUserIdByEmail(string email, string subclaim);

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

        
        public int GetUserIdByEmail(string email, string subclaim)
        {
            const string sqlCommand = "[dbo].[InsertEmailAndSubclaim]";
            int userId;
            
            using (var connection = _connection.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);                
                parameters.Add("@Subclaim", subclaim);
                parameters.Add("@AccountID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                 connection.Execute(sqlCommand, parameters, commandType: CommandType.StoredProcedure);

                userId = parameters.Get<int>("@AccountID");
            }
            
            return userId;
        }


    }
}