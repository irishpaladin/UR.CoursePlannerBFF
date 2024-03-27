using UR.CoursePlannerBFF.CourseManagerService.CourseDetails;

namespace UR.CoursePlannerBFF.CourseManagerService.Models
{
    public class CourseFacultyRequirementsModel: IRequirementCondition
    {
        public int coursefacultyrequirement_id { get; set; }
        public int requirement_id { get; set; }
        public int faculty_id { get; set; }
    }
}
