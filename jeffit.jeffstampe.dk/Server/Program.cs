using jeffit.jeffstampe.dk.Server.Hubs;
using jeffit.jeffstampe.dk.Server.Model;
using jeffit.jeffstampe.dk.Server.Services;
using jeffit.jeffstampe.dk.Server.Services.Interfaces;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IThreadService, ThreadServiceEFSQLite>();
builder.Services.AddDbContext<ThreadContext>(options =>
options.UseSqlite($"Data Source=bin/TodoTask.db"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/jeffitws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocketHub = new WebsocketHub();
            await webSocketHub.HandleConnection(context);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
