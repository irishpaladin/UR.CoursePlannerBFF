using Dapper;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.RequirementSchedule.Models;

namespace UR.CoursePlannerBFF.RequirementSchedule
{
    public interface IRequirementSchedulerApiService
    {
        public IEnumerable<RequirementScheduleModel> GetRequirementSchedulesByUserId(int userId);
        public Task<RequirementScheduleModel> SaveRequirementSchedule(RequirementScheduleModel model);
        public Task UpdateRequirementSchedule(int? requirementScheduleId, int? courseSectionId);

    }
    public class RequirementSchedulerApiService : IRequirementSchedulerApiService
    {
        private readonly IDBConnectionProvider _connection;

        public RequirementSchedulerApiService(IDBConnectionProvider connection)
        {
            _connection = connection;
        }

        public IEnumerable<RequirementScheduleModel> GetRequirementSchedulesByUserId(int accountId)
        {
            const string sqlCommand = "[dbo].[GetRequirementScheduleByUserId]";
            var sqlParameter = new
            {
                account_id = accountId,
            };
            var result = _connection.GetConnection()
                .Query<RequirementScheduleModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            return result;
        }

        public Task<RequirementScheduleModel> SaveRequirementSchedule(RequirementScheduleModel model)
        {
            const string sqlCommand = "[dbo].[SaveRequirementSchedule]";
            var sqlParameter = new
            {
                requirement_id = model.requirement_id,
                coursesection_id = model.coursesection_id,
                account_id = model.account_id,
            };
            var result = _connection.GetConnection()
                .QueryAsync<RequirementScheduleModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return Task.FromResult(result);
        }

        public Task UpdateRequirementSchedule(int? requirementScheduleId, int? courseSectionId)
        {
            const string sqlCommand = "[dbo].[UpdateRequirementSchedule]";
            var sqlParameter = new
            {
                requirementsschedules_id = requirementScheduleId,
                coursesection_id = courseSectionId,
            };
            _connection.GetConnection()
                .QueryAsync<RequirementScheduleModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return Task.CompletedTask;
        }
    }
}