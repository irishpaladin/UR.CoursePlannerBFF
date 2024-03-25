namespace UR.CoursePlannerBFF.CourseManagerService.Models
{
    public class SectionSchedules
    {
        public int sectionschedule_id { get; set; }
        public int coursesection_id { get; set; }
        public int sectionschedule_day { get; set; }
        public int sectionschedule_starttime { get; set; }
        public int sectionschedule_endtime { get; set; }

    }

}