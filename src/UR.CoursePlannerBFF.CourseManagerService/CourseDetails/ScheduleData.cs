namespace UR.CoursePlannerBFF.CourseManagerService.CourseDetails
{
    public class ScheduleData
    {
        public int sectionschedule_id { get; set; }
        public TimeSpan sectionschedule_starttime { get; set; }
        public TimeSpan sectionschedule_endtime { get; set; }
        public int sectionschedule_day { get; set; }
    }
}
