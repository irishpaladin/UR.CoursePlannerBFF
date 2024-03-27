using System.Collections.Generic;
using UR.CoursePlannerBFF.CourseManagerService.Utility;

namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public interface IGetCourseDataByRequirementId
    {
        public RequirementData GetEligibleCoursesByRequirementId(int requirementId);
        public List<RequirementData> GetEligibleCoursesByUserId(int requirementId);
    }

    public class GetCourseDataByRequirementId : IGetCourseDataByRequirementId
    {
        private readonly ICourseManagerApiService _courseManagerApiService;
        private readonly IUserManagerApiService _userManagerApiService;
        private readonly IGetEligibleCoursesByRequirementType _eligibleCoursesByRequirementType;
        public GetCourseDataByRequirementId(
            ICourseManagerApiService courseManagerApiService,
            IGetEligibleCoursesByRequirementType getEligibleCoursesByRequirementType,
            IUserManagerApiService userManagerApiService)
        {
            _courseManagerApiService = courseManagerApiService;
            _eligibleCoursesByRequirementType = getEligibleCoursesByRequirementType;
            _userManagerApiService= userManagerApiService;
        }

        public RequirementData GetEligibleCoursesByRequirementId(int requirementId)
        {
            var getCourseSubjectRequirements = _courseManagerApiService.GetCourseSubjectRequirementsByRequirementId(requirementId);
            var getCourseFacultyRequirements = _courseManagerApiService.GetCourseFacultyRequirementsIdByRequirementId(requirementId);
            var getCourseCatalogRequirements = _courseManagerApiService.GetCourseCatalogRequirementsByRequirementId(requirementId);
            var getCourseAttributeRequirements = _courseManagerApiService.GetCourseAttributeRequirementsByRequirementId(requirementId);

            if (getCourseSubjectRequirements != null)
            {
                return _eligibleCoursesByRequirementType.Handle(RequirementType.CourseSubjectRequirement, (IEnumerable<IRequirementCondition>)getCourseSubjectRequirements, requirementId);
            }
            else if (getCourseFacultyRequirements != null)
            {
                return _eligibleCoursesByRequirementType.Handle(RequirementType.CourseFacultyRequirement, (IEnumerable<IRequirementCondition>)getCourseFacultyRequirements, requirementId);
            }
            else if (getCourseCatalogRequirements != null)
            {
                return _eligibleCoursesByRequirementType.Handle(RequirementType.CourseCatalogRequirement, (IEnumerable<IRequirementCondition>)getCourseCatalogRequirements, requirementId);
            }
            else if (getCourseAttributeRequirements != null)
            {
                return _eligibleCoursesByRequirementType.Handle(RequirementType.CourseAttributeRequirement, (IEnumerable<IRequirementCondition>)getCourseAttributeRequirements, requirementId);
            }
            else
            {
                throw new Exception("No Requirement Condition taken from this Requriement Id");
            }


        }

        public List<RequirementData> GetEligibleCoursesByUserId(int userId)
        {
            return _userManagerApiService.GetRequirementsByUserId(userId)
                .Select(r => GetEligibleCoursesByRequirementId(r.requirement_id)).ToList();
        }
    }

}
