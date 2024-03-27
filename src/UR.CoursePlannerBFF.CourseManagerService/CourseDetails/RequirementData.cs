namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public class RequirementData
    {
        public int requirement_id { get; set; }
        public int requirement_occurence { get; set; }
        public int requirement_status { get; set; }
        public string requirement_name { get; set; }
        public int audit_id { get; set; }
        public List<SubjectData> requirement_details { get; set; }
    }
}
