using database;
using service;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");
var database = DatabaseConfiguration.Create(connectionString);

DatabaseInitializer.Initialize(database);

builder.Services.AddScoped<DataBase>(
    _ => DatabaseConfiguration.Create(connectionString)
);

builder.Services.AddScoped<ProductController>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddControllers();

builder.Services.AddOpenApiDocument();

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
var imageDirectory = Path.Combine(app.Environment.ContentRootPath, "product_images");
Directory.CreateDirectory(imageDirectory);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imageDirectory),
    RequestPath = "/product_images"
});
app.UseOpenApi();
app.UseSwaggerUi();
app.UseCors("React");
app.MapControllers();

app.Run();