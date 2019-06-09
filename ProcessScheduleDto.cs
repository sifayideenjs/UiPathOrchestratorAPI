using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    public class ProcessScheduleDto
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int ReleaseId { get; set; }
        public string ReleaseKey { get; set; }
        public string ReleaseName { get; set; }
        public string PackageName { get; set; }
        public string EnvironmentName { get; set; }
        public string EnvironmentId { get; set; }
        public string StartProcessCron { get; set; }
        public string StartProcessCronDetails { get; set; }
        public string StartProcessCronSummary { get; set; }
        public DateTime? StartProcessNextOccurrence { get; set; }
        public int StartStrategy { get; set; }
        public string StopProcessExpression { get; set; }
        public string StopStrategy { get; set; }
        public string ExternalJobKey { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneIana { get; set; }
        public int Id { get; set; }
    }
}