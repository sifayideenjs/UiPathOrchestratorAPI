using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPath
{
    public class EnvironmentDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }

        public RobotDto[] Robots { get; set; }
    }
}
