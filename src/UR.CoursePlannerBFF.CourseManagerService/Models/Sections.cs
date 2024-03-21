namespace UR.CoursePlannerBFF.CourseManagerService.Models
{    
    public class Sections
    {
        public int coursesection_id { get; set; }
        public int coursesection_year { get; set; }
        public int coursesection_term { get; set; }
        public int coursesection_number { get; set; }
        public int coursesection_CRN { get; set; }
        public int coursesection_maxseat { get; set; }
        public int coursesection_maxwaitlist { get; set; }
        public int? labsection_id { get; set; }
        public int? coursecatalog_id { get; set; }

    }

}