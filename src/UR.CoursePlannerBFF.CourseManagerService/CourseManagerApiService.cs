using Dapper;
using System.Data;
using UR.CoursePlannerBFF.Common.DBConnection;
using UR.CoursePlannerBFF.CourseManagerService.CourseDetails;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManagerService
{
    public interface ICourseManagerApiService
    {
        public CourseModel GetCourseById(int courseId);
        public Sections GetSectionByCourseId(int courseId);
        public Sections GetCourseByTermAndYear(int year, int term);
        public Sections GetCourseByRequirementId(int requirementId);
        public IEnumerable<CourseModel> GetAllCourses();
        public CourseModel GetCourseByName(string subject, int number);
        public IEnumerable<CourseModel> GetAllCoursesByFacultyId(int facultyId);

        //Getting all CoursesByRequriemetnId
        public Requirement RequirementsDetailsByRequirementId(int requirementId);
        public IEnumerable<CourseSubjectRequirementsModel> GetCourseSubjectRequirementsByRequirementId (int requirementId);
        public IEnumerable<CourseFacultyRequirementsModel> GetCourseFacultyRequirementsIdByRequirementId(int requirementId);
        public IEnumerable<CourseCatalogRequirementsModel> GetCourseCatalogRequirementsByRequirementId(int requirementId);
        public IEnumerable<CourseAttributeRequirementsModel> GetCourseAttributeRequirementsByRequirementId(int requirementId);

        public IEnumerable<CourseSubjectModel> GetAllCourseSubjects();
        public IEnumerable<CourseCatalog> GetCourseCatalogByCourseSubjectId(int courseSubjectId);
        public IEnumerable<Sections> GetCourseSectionsByCourseCatalogId (int courseCatalogId);
        public IEnumerable<SectionSchedules> GetSectionSchedulesByCourseSectionId(int sectionScheduleId);

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
            return result.FirstOrDefault();
        }

        public IEnumerable<CourseModel> GetAllCourses()
        {
            const string sqlCommand = "[dbo].[CourseDetails]";
            var result = _connection.GetConnection()
                .Query<CourseModel>( sqlCommand,  commandType: CommandType.StoredProcedure);
            return result;
        }

        public CourseModel GetCourseByName(string subject, int number)
        {
            var allCourses = GetAllCourses();
            var result = allCourses.Where(course => course.coursesubject == subject && course.coursecatalog_number == number);
            return result.FirstOrDefault();
        }

        public Sections GetSectionByCourseId(int courseId)
        {
            const string sqlCommand = "[dbo].[GetCourseDataById]";
            var sqlParameter = new
            {
                id = courseId
            };
            var result = _connection.GetConnection()
                .Query<Sections>( sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if(result == null ) { throw new Exception($"Course with {courseId} ID does not exist"); }
            return result.FirstOrDefault();
        }
        public Sections GetCourseByTermAndYear(int year, int term)
        {
            const string sqlCommand = "[dbo].[GetCourseDataByTermAndYear]";
            var sqlParameter = new
            {   
                ter = term,
                ye = year
            };
            var result = _connection.GetConnection()
                .Query<Sections>( sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if(result == null ) { throw new Exception($"Course with this {year} does not exist"); }
            return result.FirstOrDefault();
        }
        public Sections GetCourseByRequirementId(int requirementId)
        {
            const string sqlCommand = "[dbo].[GetCourseByRequirementId]";
            var sqlParameter = new
            {   
                rid = requirementId
            };
            var result = _connection.GetConnection()
                .Query<Sections>( sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if(result == null ) { throw new Exception($"Course with this requirement ID {requirementId} does not exist"); }
            return result.FirstOrDefault();
        }

        public IEnumerable<CourseModel> GetAllCoursesByFacultyId(int facultyId)
        {
            var allCourses = GetAllCourses();
            var result = allCourses.Where(course => course.faculty_id == facultyId);
            return result;
        }

        #region GetAllCourseData
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

        public IEnumerable<CourseSubjectRequirementsModel> GetCourseSubjectRequirementsByRequirementId(int requirementId)
        {
            const string sqlCommand = "[dbo].[GetCourseSubjectRequirementsByRequirementsId]";
            var sqlParameter = new
            {
                requirementId = requirementId
            };
            var result = _connection.GetConnection()
                .Query<CourseSubjectRequirementsModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any()) { return null; }
            return result;
        }

        public IEnumerable<CourseFacultyRequirementsModel> GetCourseFacultyRequirementsIdByRequirementId(int requirementId)
        {
            const string sqlCommand = "[dbo].[GetCourseFacultyRequirementsByRequirementsId]";
            var sqlParameter = new
            {
                requirementId = requirementId
            };
            var result = _connection.GetConnection()
                .Query<CourseFacultyRequirementsModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any()) { return null; }
            return result;
        }

        public IEnumerable<CourseCatalogRequirementsModel> GetCourseCatalogRequirementsByRequirementId(int requirementId)
        {
            const string sqlCommand = "[dbo].[GetCourseCatalogRequirementsByRequirementsId]";
            var sqlParameter = new
            {
                requirementId = requirementId
            };
            var result = _connection.GetConnection()
                .Query<CourseCatalogRequirementsModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any()) { return null; }
            return result;
        }

        public IEnumerable<CourseAttributeRequirementsModel> GetCourseAttributeRequirementsByRequirementId(int requirementId)
        {
            const string sqlCommand = "[dbo].[GetCourseAttributeRequirementsByRequirementsId]";
            var sqlParameter = new
            {
                requirementId = requirementId
            };
            var result = _connection.GetConnection()
                .Query<CourseAttributeRequirementsModel>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any()) { return null; }
            return result;
        }

        public IEnumerable<CourseSubjectModel> GetAllCourseSubjects()
        {
            const string sqlCommand = "[dbo].[GetAllCourseSubjects]";
            var result = _connection.GetConnection()
                .Query<CourseSubjectModel>(sqlCommand, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any()) { return Enumerable.Empty<CourseSubjectModel>(); }
            return result;
        }

        public IEnumerable<CourseCatalog> GetCourseCatalogByCourseSubjectId(int courseSubjectId)
        {
            const string sqlCommand = "[dbo].[GetCourseCatalogByCourseSubjectId]";
            var sqlParameter = new
            {
                id = courseSubjectId
            };
            var result = _connection.GetConnection()
                .Query<CourseCatalog>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any()) { return Enumerable.Empty<CourseCatalog>(); }
            return result;
        }

        public IEnumerable<Sections> GetCourseSectionsByCourseCatalogId(int courseCatalogId)
        {
            const string sqlCommand = "[dbo].[GetCourseSectionsByCourseCatalogId]";
            var sqlParameter = new
            {
                id = courseCatalogId
            };
            var result = _connection.GetConnection()
                .Query<Sections>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any()) { return Enumerable.Empty<Sections>(); }
            return result;
        }

        public IEnumerable<SectionSchedules> GetSectionSchedulesByCourseSectionId(int sectionScheduleId)
        {
            const string sqlCommand = "[dbo].[GetSectionSchedulesByCourseSectionId]";
            var sqlParameter = new
            {
                id = sectionScheduleId
            };
            var result = _connection.GetConnection()
                .Query<SectionSchedules>(sqlCommand, sqlParameter, commandType: CommandType.StoredProcedure);

            if (result == null || !result.Any()) { return Enumerable.Empty<SectionSchedules>(); }
            return result;
        }

        #endregion
    }
}
