using Dapper;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.RequirementSchedule.Models;

namespace UR.CoursePlannerBFF.RequirementSchedule
{
    public class RequirementScheduleApiService
    {
        public interface IRequirementSchedulerApiService
        {
            public IEnumerable<RequirementScheduleModel> GetRequirementSchedulesByUserId(int userId);

        }
        public class RequirementSchedulerApiService : IRequirementSchedulerApiService
        {
            private readonly IDBConnectionProvider _connection;

            public RequirementSchedulerApiService(IDBConnectionProvider connection)
            {
                _connection = connection;
            }

            public IEnumerable<RequirementScheduleModel> GetRequirementSchedulesByUserId(int courseId)
            {
                const string sqlCommand = "[dbo].[GetRequirementScheduleByUserId]";

                var result = _connection.GetConnection()
                    .Query<RequirementScheduleModel>(sqlCommand, commandType: CommandType.StoredProcedure);

                if (result == null) { throw new Exception($"No Requirements Schedule found"); }
                return result;
            }

        }

    }
}