using Dapper;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManagerService
{
    public interface IUserManagerApiService
    {
        public IEnumerable<Requirement> GetRequirementsByUserId(int userId);
        public IEnumerable<SectionSchedules> GetSchedulesByUserId(int userId);                
        public int SaveUserCredentials(string email, string subclaim);
        public int SaveUserFiltersByUserId(int coursesubjectid, int coursecatalogid, int accountId);
        public IEnumerable<Filter> GetUserFiltersByUserId(int userId);
        public User GetUserInfoBySubclaim(string subclaim);
        public int? RetrieveUserId(string email, string subclaim);

    }
    public class UserManagerApiService : IUserManagerApiService
    {
        private readonly IDBConnectionProvider _connection;

        public UserManagerApiService(IDBConnectionProvider connection)
        {
            _connection = connection;
        }

        public IEnumerable<Requirement> GetRequirementsByUserId(int userId)
        {
            const string sqlCommand = "[dbo].[GetRequirementsByUserId]";
            var sqlParameter = new
            {
                userId = userId
            };
            var result = _connection.GetConnection()
                .Query<Requirement>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null) { throw new Exception($"Requirements with {userId} userId does not exist"); }
            return result;
        }
        public IEnumerable<SectionSchedules> GetSchedulesByUserId(int userId)
        {
            const string sqlCommand = "[dbo].[GetSchedulesByUserId]";
            var sqlParameter = new
            {
                userId = userId
            };
            var result = _connection.GetConnection()
                .Query<SectionSchedules>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null) { throw new Exception($"Schedules from {userId} userId does not exist"); }
            return result;

        }
        public IEnumerable<Filter> GetUserFiltersByUserId(int userId)
        {
            const string sqlCommand = "[dbo].[GetUserFiltersByUserId]";
            var sqlParameter = new
            {
                userId = userId
            };
            var result = _connection.GetConnection()
                .Query<Filter>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any())
            {
                throw new Exception($"Filters for user with ID {userId} do not exist");
            }
            return result;
        }

        public int SaveUserFiltersByUserId(int coursesubjectid, int coursecatalogid, int accountId)
        {

            const string sqlCommand = "[dbo].[SaveUserFiltersByUserId]";
            int userId;
            using (var connection = _connection.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@coursesubjectid", coursesubjectid);
                parameters.Add("@coursecatalogid", coursecatalogid);
                parameters.Add("@accountid", accountId);

                connection.Execute(sqlCommand, parameters, commandType: CommandType.StoredProcedure);

                userId = accountId;
            }
            return userId;

        }

        public User GetUserInfoBySubclaim(string subclaim)
        {
            const string sqlCommand = "[dbo].[GetUserInfoBySubclaim]";
            var sqlParameter = new DynamicParameters();
            sqlParameter.Add("@subclaim", subclaim); // Add the subclaim parameter

            var result = _connection.GetConnection()
                .Query<User>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
        }

        public int SaveUserCredentials(string email, string subclaim)
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

        public int? RetrieveUserId(string email, string subclaim)
        {
            User? user = GetUserInfoBySubclaim(subclaim);
            if (user != null)
                return user?.account_id;
            else
                return SaveUserCredentials(email, subclaim);
        }

        public Requirement RequirementsDetailsByRequirementId(int requirementId)
        {
            const string sqlCommand = "[dbo].[GetRequirementsByRequirementsId]";
            var sqlParameter = new
            {
                requirementId = requirementId
            };
            var result = _connection.GetConnection()
                .Query<Requirement>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null) { throw new Exception($"Requirements with {requirementId} does not exist"); }
            return result.FirstOrDefault();
        }
    }
}