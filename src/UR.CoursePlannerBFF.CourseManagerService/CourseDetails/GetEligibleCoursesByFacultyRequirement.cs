
using UR.CoursePlannerBFF.CourseManagerService.Models;
using UR.CoursePlannerBFF.CourseManagerService.Utility;

namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public interface IGetEligibleCoursesByFacultyRequirement
    {
        public RequirementData Handle(IEnumerable<CourseFacultyRequirementsModel> condition, int requirementId);
    }
    public class GetEligibleCoursesByFacultyRequirement : IGetEligibleCoursesByFacultyRequirement
    {
        readonly ICourseManagerApiService _courseManagerApiService;
        public GetEligibleCoursesByFacultyRequirement(ICourseManagerApiService courseManagerApiService)
        {
            _courseManagerApiService = courseManagerApiService;
        }

        public RequirementData Handle(IEnumerable<CourseFacultyRequirementsModel> condition, int requirementId)
        {
            var allCoursesData = _courseManagerApiService.GetAllCourseSubjects()
                .Select(c => c.MapToSubjectDataData()).ToList();

            foreach (var data in allCoursesData)
            {
                data.coursecatalogs =
                    _courseManagerApiService.GetCourseCatalogByCourseSubjectId(data.coursesubject_id)
                        .Where(courseCatalogs => 
                            condition.Where(condition => condition.faculty_id == courseCatalogs.faculty_id).Any())
                        .ToList()
                        .Select(cc => cc.MaptoCourseCatalogData()).ToList();
                foreach (var catalogs in data.coursecatalogs)
                {
                    catalogs.sections =
                        _courseManagerApiService.GetCourseSectionsByCourseCatalogId(catalogs.coursecatalog_id)
                        .Select(cs => cs.MapToSectionData()).ToList();

                    foreach (var section in catalogs.sections)
                    {
                        section.schedule =
                            _courseManagerApiService.GetSectionSchedulesByCourseSectionId(section.coursesection_id)
                            .Select(s => s.MapToScheduleData()).ToList();
                    }
                }

            }
            var requirements = _courseManagerApiService.RequirementsDetailsByRequirementId(requirementId);
            return allCoursesData.Where(c => c.coursecatalogs.Any()).ToList().MapToRequirementData(requirements);
        }
    }
}
