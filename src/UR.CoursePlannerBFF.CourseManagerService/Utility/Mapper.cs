using UR.CoursePlannerBFF.CourseManagerService.CourseDetails;
using UR.CoursePlannerBFF.CourseManagerService.Models;

namespace UR.CoursePlannerBFF.CourseManagerService.Utility
{
    public static class Mapper
    {
        public static RequirementData MapToRequirementData(this List<SubjectData> subjectdataList, Requirement requirementData)
        {
            return new RequirementData
            {
                requirement_id = requirementData.requirement_id,
                requirement_occurence = requirementData.requirement_occurence,
                requirement_status = requirementData.requirement_status,
                requirement_name = requirementData.requirement_name,
                audit_id = requirementData.audit_id,
                requirement_details = subjectdataList,
            };
        }
        public static SubjectData MapToSubjectDataData(this CourseSubjectModel course)
        {
            return new SubjectData
            {
                coursesubject_id = course.coursesubject_id,
                coursesubject_name = course.coursesubject_name,
                coursesubject = course.coursesubject
            };
        }

        public static CourseCatalogData MaptoCourseCatalogData(this CourseCatalog courseCatalog)
        {
            return new CourseCatalogData
            {
                coursecatalog_id = courseCatalog.coursecatalog_id,
                coursecatalog_name = courseCatalog.coursecatalog_name,
                coursecatalog_number = courseCatalog.coursecatalog_number,
                faculty_id = courseCatalog.faculty_id,
                courseattribute_id = courseCatalog.courseattribute_id,
            };
        }

        public static SectionData MapToSectionData(this Sections sections)
        {
            return new SectionData
            {
                coursesection_id = sections.coursesection_id,
                coursesection_year = sections.coursesection_year,
                coursesection_term = sections.coursesection_term,
                coursesection_number = sections.coursesection_number,
                coursesection_CRN = sections.coursesection_CRN,
                coursesection_maxseat = sections.coursesection_maxseat,
                coursesection_maxwaitlist = sections.coursesection_maxwaitlist,
                labsection_id = sections.labsection_id
            };
        }

        public static ScheduleData MapToScheduleData(this SectionSchedules section)
        {
            return new ScheduleData
            {
                sectionschedule_id = section.sectionschedule_id,
                sectionschedule_starttime = section.sectionschedule_starttime,
                sectionschedule_endtime = section.sectionschedule_endtime,
                sectionschedule_day = section.sectionschedule_day
            };
        }
    }

}
