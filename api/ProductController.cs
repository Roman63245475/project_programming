using api;
using be;
using Microsoft.AspNetCore.Mvc;
using service;

public class ProductController : ControllerBase
{
    private ProductService productService;
    private readonly IWebHostEnvironment webHostEnvironment;
    public ProductController(ProductService productService, IWebHostEnvironment webHostEnvironment)
    {
        this.productService = productService;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpPost(nameof(create_product))]
    public async Task<IActionResult> create_product([FromForm] ProductDTO productDTO)
    {
        var imgPath = await ProcessImage(productDTO.image);
        
        var product = new Product
        {
            name = productDTO.name,
            price = productDTO.price,
            quantity = productDTO.quantity,
            category_id = productDTO.category_id,
            available = productDTO.available,
            image_path = imgPath
        };
        await productService.create_product(product);
        return Ok();
    }

    [HttpGet(nameof(GetProducts))]
    public async Task<List<Product>> GetProducts([FromQuery] CategoryDTO categoryDTO) {
       return await productService.GetProducts(categoryDTO);
    }

    [HttpGet("{id}")]
    public async Task<Product> GetProduct([FromRoute] int id) {
        return await productService.GetProduct(id);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id) {
        await productService.Delete(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromForm] ProductDTO productDTO, [FromRoute] int id) {
        var imgPath = await ProcessImage(productDTO.image);
        
        var product = new Product
        {
            name = productDTO.name,
            price = productDTO.price,
            quantity = productDTO.quantity,
            category_id = productDTO.category_id,
            available = productDTO.available,
            image_path = imgPath
        };
        
        await productService.Update(product, id);
        return Ok();
    }

    private async Task<string> ProcessImage(IFormFile image) {
        if (image == null) return "";
        var dir = "product_images";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        var imageDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "product_images");
        Directory.CreateDirectory(imageDirectory);
        
        var file_name = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var file_path = Path.Combine(imageDirectory, file_name);
        await using var stream = System.IO.File.Create(file_path);
        await image.CopyToAsync(stream);
        
        Console.WriteLine("file saved with name of ${file_name} aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        return $"product_images/{file_name}";
    }
}