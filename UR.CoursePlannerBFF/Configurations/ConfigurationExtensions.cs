using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.CourseManagerService;
using UR.CoursePlannerBFF.RequirementSchedule;


namespace UR.CoursePlannerBFF.Configurations
{
    public static class ConfigurationExtensions
    {
        public static void AddConfigurationOptions(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;
            builder.Services.Configure<DBConnectionOptions>(config.GetSection(DBConnectionOptions.SQLDatabase));
        }
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IDBConnectionProvider, DBConnectionProvider>();
            builder.Services.AddScoped<ICourseManagerApiService, CourseManagerApiService>();
            builder.Services.AddScoped<IRequirementSchedulerApiService, RequirementSchedulerApiService>();
            builder.Services.AddScoped<IUserManagerApiService, UserManagerApiService>();
            builder.Services.AddScoped<IFacultyManagerApiService, FacultyManagerApiService>();

        }
    }
}
