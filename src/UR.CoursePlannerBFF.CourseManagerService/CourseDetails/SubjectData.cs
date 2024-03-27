using UR.CoursePlannerBFF.CourseManagerService.CourseDetails;

namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public class SubjectData
    {
        public int coursesubject_id { get; set; }
        public string coursesubject_name { get; set; }
        public string coursesubject { get; set; }
        public List<CourseCatalogData> coursecatalogs { get; set; } = new List<CourseCatalogData>();
    }
}
