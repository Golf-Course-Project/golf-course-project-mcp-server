using GcpMcpServer.Requests;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GcpMcpServer.Tools;

[McpServerToolType]
public static class CourseTool
{
    [McpServerTool, Description("Get top golf courses in state")]
    public static async Task<string> GetTopCoursesByState(
        HttpClient client, 
        [Description("The US state code to get courses for")] string state)
    {
        CourseListRequest request = new CourseListRequest
        {
            State = state,
            Tiers = new[] { "I", "II" }
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("/api/course/list", request);

        if (response.IsSuccessStatusCode)
        {
            CourseListResponse? responseObj = await response.Content.ReadFromJsonAsync<CourseListResponse>();

            if (responseObj?.Value != null && responseObj.Value.Count > 0)
            {
                return string.Join("\n---\n", responseObj.Value.Select(course =>
                    $"{course.CourseName} ({course.City})"));
            }
            else
            {
                return $"No top courses found for state: {state}.";
            }
        }
        else
        {
            return $"❌ Request failed: {response.StatusCode}";
        }
    }

    [McpServerTool, Description("Get fetured golf courses by state")]
    public static async Task<string> GetFeaturedByState(
        HttpClient client,
        [Description("The US state code to get courses for")] string state,
        [Description("Number of courses to return. Default is 10")] double top = 10)
    {
        try
        {
            var responseObj = await client.GetFromJsonAsync<CourseListResponse>($"/api/course/list/featured?state={state}&top={top}");

            if (responseObj?.Value != null && responseObj.Value.Count > 0)
            {
                return string.Join("\n---\n", responseObj.Value.Select(course =>
                    $"{course.CourseName} ({course.City})"));
            }
            else
            {
                return $"No featured courses found for state: {state}.";
            }
        }
        catch (HttpRequestException ex)
        {
            return $"❌ Request failed: {ex.Message}";
        }
    }

    //[McpServerTool, Description("Get featured courses")]
    //public static async Task<string> GetForecast(
    //    HttpClient client,
    //    [Description("Latitude of the location.")] double latitude,
    //    [Description("Longitude of the location.")] double longitude)
    //{
    //    var jsonElement = await client.GetFromJsonAsync<JsonElement>($"/points/{latitude},{longitude}");
    //    var periods = jsonElement.GetProperty("properties").GetProperty("periods").EnumerateArray();

    //    return string.Join("\n---\n", periods.Select(period => $"""
    //                    {period.GetProperty("name").GetString()}
    //                    Temperature: {period.GetProperty("temperature").GetInt32()}°F
    //                    Wind: {period.GetProperty("windSpeed").GetString()} {period.GetProperty("windDirection").GetString()}
    //                    Forecast: {period.GetProperty("detailedForecast").GetString()}
    //                    """));
    //}
}
