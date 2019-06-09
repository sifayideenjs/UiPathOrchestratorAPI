using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    public class ReleaseDto
    {
        public string Key { get; set; }
        public string ProcessKey { get; set; }
        public string ProcessVersion { get; set; }
        public bool IsLatestVersion { get; set; }
        public bool IsProcessDeleted { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int EnvironmentId { get; set; }
        public string EnvironmentName { get; set; }
        public int Id { get; set; }
    }
}
