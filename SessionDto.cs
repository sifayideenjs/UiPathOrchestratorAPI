using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    public class SessionDto
    {
        public string State { get; set; }
        public string ReportingTime { get; set; }
        public string Info { get; set; }
        public string IsUnresponsive { get; set; }
        public int Id { get; set; }
        public RobotDto Robot { get; set; }
    }
}
