using UR.CoursePlannerBFF.CourseManagerService.Models;
using UR.CoursePlannerBFF.CourseManagerService.Utility;

namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public interface IGetEligibleCoursesByRequirementType
    {
        public RequirementData Handle(RequirementType requriementType, IEnumerable<IRequirementCondition> condition, int requirementId);
    }
    public class GetEligibleCoursesByRequirementType : IGetEligibleCoursesByRequirementType
    {
        private readonly IGetEligibleCoursesByCourseAttributeRequirement _getEligibleCoursesByCourseAttributeRequirement;
        private readonly IGetEligibleCoursesByCourseCatalogRequirement _getEligibleCoursesByCourseCatalogRequirement;
        private readonly IGetEligibleCoursesByFacultyRequirement _getEligibleCoursesByFacultyRequirement;
        private readonly IGetEligibleCoursesBySubjectRequirements _getEligibleCoursesBySubjectRequirements;
        public GetEligibleCoursesByRequirementType(
                IGetEligibleCoursesByCourseAttributeRequirement getEligibleCoursesByCourseAttributeRequirement,
                IGetEligibleCoursesByCourseCatalogRequirement getEligibleCoursesByCourseCatalogRequirement,
                IGetEligibleCoursesByFacultyRequirement getEligibleCoursesByFacultyRequirement,
                IGetEligibleCoursesBySubjectRequirements getEligibleCoursesBySubjectRequirements
            )
        {
            _getEligibleCoursesByCourseAttributeRequirement = getEligibleCoursesByCourseAttributeRequirement;
            _getEligibleCoursesByCourseCatalogRequirement = getEligibleCoursesByCourseCatalogRequirement;
            _getEligibleCoursesByFacultyRequirement = getEligibleCoursesByFacultyRequirement;
            _getEligibleCoursesBySubjectRequirements = getEligibleCoursesBySubjectRequirements;

        }
        public RequirementData Handle(RequirementType requriementType, IEnumerable<IRequirementCondition> condition, int requirementId)
        {
            switch (requriementType)
            {
                case RequirementType.CourseAttributeRequirement:
                    return _getEligibleCoursesByCourseAttributeRequirement.Handle((IEnumerable<CourseAttributeRequirementsModel>)condition, requirementId);
                case RequirementType.CourseCatalogRequirement:
                    return _getEligibleCoursesByCourseCatalogRequirement.Handle((IEnumerable<CourseCatalogRequirementsModel>)condition, requirementId);
                case RequirementType.CourseFacultyRequirement:
                    return _getEligibleCoursesByFacultyRequirement.Handle((IEnumerable<CourseFacultyRequirementsModel>)condition, requirementId);
                case RequirementType.CourseSubjectRequirement:
                    return _getEligibleCoursesBySubjectRequirements.Handle((IEnumerable<CourseSubjectRequirementsModel>)condition, requirementId);
            }
            throw new Exception("Error picking requirement type");
        }
    }
}
