using be;
using database;
using Microsoft.AspNetCore.Mvc;
using service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");
var database = DatabaseConfiguration.Create(connectionString);

builder.Services.AddSingleton<DataBase>(_ => DatabaseConfiguration.Create(connectionString));

builder.Services.AddScoped<ProductController>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddControllers();

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


public class ProductController : ControllerBase
{
    private ProductService productService;
    public ProductController(ProductService productService)
    {
        this.productService = productService;
    }

    [HttpPost(nameof(create_product))]
    public void create_product([FromBody] Product product)
    {
        Console.WriteLine(product.name);
    }
}