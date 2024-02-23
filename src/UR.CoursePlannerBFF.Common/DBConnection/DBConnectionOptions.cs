using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UR.CoursePlannerBFF.Common.DBConnection
{
    public class DBConnectionOptions
    {
        public const string SQLDatabase = "SQLDatabase";
        public string ConnectionStrings { get; set; } = string.Empty;
    }
}
