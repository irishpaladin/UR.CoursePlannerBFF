using System;
using UR.CoursePlannerBFF.CourseManagerService.CourseDetails;

namespace UR.CoursePlannerBFF.CourseManagerService.Models
{
    public class CourseSubjectRequirementsModel: IRequirementCondition
    {
        public int subjectrequirement_id { get; set; }
        public bool subjectrequirement_isequal { get; set; }
        public int? courserequirement_minlevel { get; set; }
        public int requirement_id { get; set; }
        public int coursesubject_id { get; set; }
    }
}
