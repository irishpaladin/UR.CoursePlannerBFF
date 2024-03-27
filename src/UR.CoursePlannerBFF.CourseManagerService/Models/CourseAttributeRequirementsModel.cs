using UR.CoursePlannerBFF.CourseManagerService.CourseDetails;

namespace UR.CoursePlannerBFF.CourseManagerService.Models
{
    public class CourseAttributeRequirementsModel: IRequirementCondition
    {
        public int courseattributerequirement_id { get; set; }
        public int requirement_id { get; set; }
        public int courseattribute_id { get; set; }
    }
}
