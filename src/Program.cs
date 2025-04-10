﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol;
using System.Net.Http.Headers;

var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddSingleton(_ =>
{
    var client = new HttpClient() { BaseAddress = new Uri("https://golfcourseproject-courseservice-public.azurewebsites.net") };
    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("gcp-mcp-server", "1.0"));
    return client;
});

var app = builder.Build();

await app.RunAsync();