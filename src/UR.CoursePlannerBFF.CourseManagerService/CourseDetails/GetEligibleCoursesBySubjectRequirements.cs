using UR.CoursePlannerBFF.CourseManagerService.Models;
using UR.CoursePlannerBFF.CourseManagerService.Utility;

namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public interface IGetEligibleCoursesBySubjectRequirements
    {
        public RequirementData Handle(IEnumerable<CourseSubjectRequirementsModel> condition, int requirementId);
    }
    public class GetEligibleCoursesBySubjectRequirements : IGetEligibleCoursesBySubjectRequirements
    {
        readonly ICourseManagerApiService _courseManagerApiService;
        public GetEligibleCoursesBySubjectRequirements(ICourseManagerApiService courseManagerApiService)
        {
            _courseManagerApiService = courseManagerApiService;
        }

        public RequirementData Handle(IEnumerable<CourseSubjectRequirementsModel> condition, int requirementId)
        {
            var allCoursesData = _courseManagerApiService.GetAllCourseSubjects()
                .Where(s => IsEligible(s, condition))
                .Select(c => c.MapToSubjectDataData()).ToList();

            foreach (var data in allCoursesData)
            {
                data.coursecatalogs =
                    _courseManagerApiService.GetCourseCatalogByCourseSubjectId(data.coursesubject_id)
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
            return allCoursesData.MapToRequirementData(requirements);
        }

        private bool IsEligible(CourseSubjectModel model, IEnumerable<CourseSubjectRequirementsModel> condition)
        {
            if (condition.First().subjectrequirement_isequal)
            {
                foreach (var c in condition)
                {
                    if (model.coursesubject_id == c.coursesubject_id)
                        return true;
                }
                return false;
            }
            else
            {
                foreach (var c in condition)
                {
                    if (model.coursesubject_id == c.coursesubject_id)
                        return false;
                }
                return true;
            }

        }
    }
}
