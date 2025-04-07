using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcpMcpServer.Requests
{
    internal class CourseListRequest
    {
        public required string State { get; set; }
        public required string[] Tiers { get; set; }
    }

}
