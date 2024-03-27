namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public class SectionData
    {
        public int coursesection_id { get; set; }
        public int coursesection_year { get;set; }
        public int coursesection_term { get; set; }
        public int coursesection_number { get; set; }
        public int coursesection_CRN { get; set; }
        public int coursesection_maxseat { get; set; }
        public int coursesection_maxwaitlist { get; set; }
        public int? labsection_id { get; set; }
        public List<ScheduleData> schedule { get; set;}  = new List<ScheduleData>();
    }
}
