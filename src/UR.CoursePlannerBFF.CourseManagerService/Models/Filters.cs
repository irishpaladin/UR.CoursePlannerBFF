namespace UR.CoursePlannerBFF.CourseManagerService.Models
{
    public class Filter
    {
        public int coursecatalogfilter_id { get; set; }
        public int account_id { get; set; }
        public int coursecatalog_id { get; set; }
        public int coursesubjectfilter_id { get; set; }
        public int coursesubject_id { get; set; }
        public int coursecatalog_number { get; set; }
        public int faculty_id { get; set; }
        public int courseattribute_id { get; set; }
        public string coursecatalog_name { get; set; }

    }

}