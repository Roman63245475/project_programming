using be;
using database;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");
var database = DatabaseConfiguration.Create(connectionString);

DatabaseInitializer.Initialize(database);

builder.Services.AddSingleton<DataBase>(_ => DatabaseConfiguration.Create(connectionString));

builder.Services.AddScoped<ProductController>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
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
    public async Task<IActionResult> create_product([FromBody] Product product)
    {
        await productService.create_product(product);
        return Ok();
    }
}

[Route("api/[controller]")]
public class CategoryController(CategoryService categoryService) : ControllerBase {
    [HttpPost(nameof(CreateCategory))]
    public async Task<IActionResult> CreateCategory([FromBody] Category category) {
        await categoryService.CreateCategory(category);
        return Ok();
    }
}