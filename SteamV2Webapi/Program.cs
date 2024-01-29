using Microsoft.EntityFrameworkCore;
using PTHUWEBAPI.Database;
using System;

var builder = WebApplication.CreateBuilder(args);



// Szolgáltatások hozzáadása a konténerhez.

builder.Services.AddControllers();
// Swagger/OpenAPI konfigurációja: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
/*
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.UseHttps("certificate.pfx", "tibor@200616");
    });
});
*/
var app = builder.Build();

// HTTP kérési csõvezeték konfigurálása.

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(options => options
    .WithOrigins("*")
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        // Logolás példája
        Console.WriteLine("OPTIONS request received");

        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
        context.Response.ContentType = "charset=utf-8";
        context.Response.StatusCode = 200;
    }
    else
    {
        //LOG
        Console.WriteLine($"{context.Request.Method} request received for {context.Request.Path} " +
            $" => {DateTime.Now}");

        await next();
    }
});


app.UseAuthorization();
app.MapControllers();
app.UseHsts();
app.Run();
