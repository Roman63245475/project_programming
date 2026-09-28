using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<TestApiController>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("React", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("React");
app.MapControllers();

app.Run();


public class TestApiController : ControllerBase
{
    [HttpGet(nameof(test_con))]
    public string test_con()
    {
        return """{"response": "Everything's fine"}""";
    }
}