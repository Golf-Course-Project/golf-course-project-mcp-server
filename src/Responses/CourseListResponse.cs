
using GcpMcpServer.Models;
using System.Text.Json.Serialization;

namespace GcpMcpServer.Requests
{
    internal class CourseListResponse
    {       
        [JsonPropertyName("value")]
        public List<Course> Value { get; set; } = new List<Course>();
    }
}

