using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    public class JobDto
    {
        public string Key { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string State { get; set; }
        public string Source { get; set; }
        public string BatchExecutionKey { get; set; }
        public string Info { get; set; }
        public string CreationTime { get; set; }
        public string StartingScheduleId { get; set; }
        public int Id { get; set; }

        public RobotDto Robot { get; set; }
        public ReleaseDto Release { get; set; }
    }
}
