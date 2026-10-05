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
        var imgPath = "";
        if (productDTO.image != null)
        {
            var dir = "product_images";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var imageDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "product_images");
            Directory.CreateDirectory(imageDirectory);
            
            //var file_name = Guid.NewGuid() + Path.GetExtension(productDTO.image.FileName);
            var file_name = $"{Guid.NewGuid()}{Path.GetExtension(productDTO.image.FileName)}";
            var file_path = Path.Combine(imageDirectory, file_name);
            await using var stream = System.IO.File.Create(file_path);
            await productDTO.image.CopyToAsync(stream);
            
            imgPath = $"product_images/{file_name}";
            Console.WriteLine("file saved with name of ${file_name} aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

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
    public List<Product> GetProducts([FromQuery] CategoryDTO categoryDTO) {
       return productService.GetProducts(categoryDTO);
    }

    [HttpGet(nameof(GetProduct))]
    public Product GetProduct(int id)
    {
        return productService.GetProduct(id);
    }
}