using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UR.CoursePlannerBFF.RequirementSchedule.Models
{
    public class RequirementScheduleModel
    {
        public int requirementsschedules_id { get; set; }
        public int requirement_id { get; set; }
        public int vourse_section_id {get;set;}
        public int account_id { get; set; }

    }
}
