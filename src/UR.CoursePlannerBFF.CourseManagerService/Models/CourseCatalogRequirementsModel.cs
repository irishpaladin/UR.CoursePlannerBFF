using UR.CoursePlannerBFF.CourseManagerService.CourseDetails;

namespace UR.CoursePlannerBFF.CourseManagerService.Models
{
    public class CourseCatalogRequirementsModel: IRequirementCondition
    {
        public int courserequirement_id { get; set; }
        public int requirement_id { get; set; }
        public int coursecatalog_id { get; set; }
    }
}
