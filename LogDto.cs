using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    public class LogDto
    {
        public string Level { get; set; }
        public string WindowsIdentity { get; set; }
        public string ProcessName { get; set; }
        public string TimeStamp { get; set; }
        public string Message { get; set; }
        public string JobKey { get; set; }
        public string RawMessage { get; set; }
        public int Id { get; set; }
    }
}
