using Microsoft.EntityFrameworkCore;
using PTHUWEBAPI.Database;
using GameHubAPI;

var builder = WebApplication.CreateBuilder(args);


// Szolgáltatások hozzáadása a konténerhez.

builder.Services.AddControllers();
// Swagger/OpenAPI konfigurációja: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new HeartBeatSettings()
{
    Frequency = TimeSpan.FromSeconds(5),
    Target = "http://192.168.1.148:5000/User/updateHeartBeats"
});
builder.Services.AddHostedService<HeartBeatChecker>();


var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


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
    {;

        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
        context.Response.ContentType = "charset=utf-8";
        context.Response.StatusCode = 200;
    }
    await next();
});
    


app.UseAuthorization();
app.MapControllers();
app.UseHsts();
app.Run();