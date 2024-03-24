using Dapper;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.RequirementSchedule.Models;

namespace UR.CoursePlannerBFF.RequirementSchedule
{
    public interface IRequirementSchedulerApiService
    {
        public IEnumerable<RequirementScheduleModel> GetRequirementSchedulesByUserId(int userId);
        public Task<RequirementScheduleModel> SaveUpdateRequirementSchedule(RequirementScheduleModel model);

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
            throw new NotImplementedException("");
            const string sqlCommand = "[dbo].[GetRequirementScheduleByUserId]";

            var result = _connection.GetConnection()
                .Query<RequirementScheduleModel>(sqlCommand, commandType: CommandType.StoredProcedure);

            return result;
        }

        public Task<RequirementScheduleModel> SaveUpdateRequirementSchedule(RequirementScheduleModel model)
        {
            return Task.FromResult(model);
            if (model.requirementsschedules_id == null)
            {
                return SaveRequirementSchedule(model);
            }
            else
            {
                return UpdateRequirementSchedule(model.requirementsschedules_id, model.vourse_section_id);
            }
        }

        private Task<RequirementScheduleModel> SaveRequirementSchedule(RequirementScheduleModel model)
        {
            const string sqlCommand = "[dbo].[SaveRequirementSchedule]";
            var sqlParameter = new
            {
                requirementsschedules_id = model.requirementsschedules_id,
                requirement_id = model.requirement_id,
                vourse_section_id = model.vourse_section_id,
                account_id = model.account_id,
            };
            var result = _connection.GetConnection()
                .QueryAsync<RequirementScheduleModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return Task.FromResult(result);
        }

        private Task<RequirementScheduleModel> UpdateRequirementSchedule(int requirementScheduleId, int courseSectionId)
        {
            const string sqlCommand = "[dbo].[UpdateRequirementSchedule]";
            var sqlParameter = new
            {
                requirementsschedules_id = requirementScheduleId,
                vourse_section_id = courseSectionId,
            };
            var result = _connection.GetConnection()
                .QueryAsync<RequirementScheduleModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return Task.FromResult(result);
        }
    }


}