using Microsoft.EntityFrameworkCore;
using PTHUWEBAPI.Database;
using System;

var builder = WebApplication.CreateBuilder(args);



// Szolg�ltat�sok hozz�ad�sa a kont�nerhez.

builder.Services.AddControllers();
// Swagger/OpenAPI konfigur�ci�ja: https://aka.ms/aspnetcore/swashbuckle
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

// HTTP k�r�si cs�vezet�k konfigur�l�sa.

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
        // Logol�s p�ld�ja
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
