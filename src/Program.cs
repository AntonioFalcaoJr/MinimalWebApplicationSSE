using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(options
        => options.AddPolicy(
            name: "cors",
            configurePolicy: policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                policyBuilder.AllowAnyOrigin();
            }));

await using var app = builder.Build();

app.UseCors("cors");

app.MapGet(
    pattern: "/sse",
    requestDelegate: async context =>
    {
        context.Response.Headers.Add("Content-Type", "text/event-stream");

        while (true)
        {
            await context.Response.WriteAsync($"{DateTimeOffset.Now}\n");
            await context.Response.Body.FlushAsync();
            await Task.Delay(500);
        }
    });

await app.RunAsync();