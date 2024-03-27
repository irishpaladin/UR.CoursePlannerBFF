
namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public class CourseCatalogData
    {
        public int coursecatalog_id { get; set; }
        public string coursecatalog_name { get; set; }
        public int coursecatalog_number { get; set; }
        public int faculty_id { get; set; }
        public int courseattribute_id { get; set;}
        public List<SectionData> sections { get; set; } = new List<SectionData>();
    }
}
