using System.ComponentModel.DataAnnotations;
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
        try
        {
            await productService.create_product(product);
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet(nameof(GetProductsByCategoryId))]
    public List<Product> GetProductsByCategoryId([FromQuery] CategoryDTO categoryDTO) {
       return productService.GetProductsByCategoryId(categoryDTO);
    }

    [HttpGet(nameof(GetProducts))]
    public async Task<List<Product>> GetProducts() {
       return await productService.GetProducts();
    }

    [HttpGet("{id}")]
    public async Task<Product> GetProduct([FromRoute] int id) {
        return productService.GetProduct(id);
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
    [HttpPatch(nameof(purchase))]
    public async Task<IActionResult> purchase(int id, int purchase_quantity)
    {
        await productService.purchase(id, purchase_quantity);
        return Ok();
    }

    [HttpDelete(nameof(fbi_caught))]
    public async Task<IActionResult> fbi_caught(int id)
    {
        await productService.fbi_caught(id);
        return Ok();
    }
}