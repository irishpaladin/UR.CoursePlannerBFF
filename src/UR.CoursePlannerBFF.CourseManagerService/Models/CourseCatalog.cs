namespace UR.CoursePlannerBFF.CourseManagerService.Models
{
    public class CourseCatalog
    {
        public int coursecatalog_id { get; set; }
        public string? coursecatalog_name { get; set; }
        public int coursecatalog_number { get; set; }
        public int faculty_id { get; set; }
        public int courseattribute_id { get; set; }
        public int coursesubject_id { get; set; }


    }

}