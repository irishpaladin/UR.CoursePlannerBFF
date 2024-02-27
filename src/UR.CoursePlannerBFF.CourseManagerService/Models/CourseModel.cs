namespace UR.CoursePlannerBFF.CourseManagerService.Models
{    
    public class CourseModel
    {
        public int course_id { get; set; }
        public string course_name { get; set; }
        public string course_subject { get; set; }
        public int course_number { get; set; }
        public int? faculty_id { get; set; }
        public int? courseattribute_id { get; set; }
    }

}