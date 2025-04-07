using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GcpMcpServer.Models
{
    internal class Course
    {
        [JsonPropertyName("courseName")]
        public required string CourseName { get; set; }

        [JsonPropertyName("facilityName")]
        public required string FacilityName { get; set; }

        [JsonPropertyName("city")]
        public required string City { get; set; }
    }
}
