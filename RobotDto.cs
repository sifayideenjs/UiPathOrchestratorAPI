using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    //public class RobotDto
    //{
    //    public string LicenseKey { get; set; }
    //    public string MachineName { get; set; }
    //    public string Name { get; set; }
    //    public string Username { get; set; }
    //    public string Description { get; set; }
    //    public string Type { get; set; }
    //    public string Password { get; set; }
    //    public string RobotEnvironments { get; set; }
    //    public int Id { get; set; }
    //}

    public class RobotDto
    {
        public string LicenseKey { get; set; }
        public string MachineName { get; set; }
        public int MachineId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public string HostingType { get; set; }
        public string Password { get; set; }
        public string CredentialType { get; set; }
        public IList<Environment> Environments { get; set; }
        public string RobotEnvironments { get; set; }
        public ExecutionSettings ExecutionSettings { get; set; }
        public int Id { get; set; }
    }

    public class Environment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<RobotDto> Robots { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
    }

    public class ExecutionSettings
    {
    }
}
