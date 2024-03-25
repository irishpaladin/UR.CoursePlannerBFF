namespace UR.CoursePlannerBFF.CourseManagerService.Models
{
    public class Requirement
    {
        public int requirement_id { get; set; }
        public int requirement_occurence { get; set; }
        public int requirement_status { get; set; }
        public string requirement_name { get; set; }
        public int audit_id { get; set; }

    }

}