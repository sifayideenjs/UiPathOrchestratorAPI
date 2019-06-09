using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    //public class QueueDefinitionDto
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public int MaxNumberOfRetries { get; set; }
    //    public bool AcceptAutomaticallyRetry { get; set; }
    //    public bool ArchiveItems { get; set; }
    //    public string CreationTime { get; set; }
    //    public int Id { get; set; }
    //}

    public class QueueDefinitionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfRetries { get; set; }
        public bool AcceptAutomaticallyRetry { get; set; }
        public bool EnforceUniqueReference { get; set; }
        public bool ArchiveItems { get; set; }
        public DateTime CreationTime { get; set; }
        public int ReleaseId { get; set; }
        public int ProcessScheduleId { get; set; }
        public string ReleaseName { get; set; }
        public int Id { get; set; }
    }
}
