using api;
using be;
using Microsoft.AspNetCore.Mvc;
using service;

public class ProductController : ControllerBase
{
    private ProductService productService;
    
    public ProductController(ProductService productService)
    {
        this.productService = productService;
    }

    [HttpPost(nameof(create_product))]
    public async Task<IActionResult> create_product([FromForm] ProductDTO productDTO)
    {
        var file_path = "";
        if (productDTO.image != null)
        {
            var dir = "product_images";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            
            var file_name = Guid.NewGuid() + Path.GetExtension(productDTO.image.FileName);
            file_path = Path.Combine(dir, file_name);
            await using var stream = System.IO.File.Create(file_path);
            await productDTO.image.CopyToAsync(stream);
            Console.WriteLine("file saved with name of ${file_name} aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        var product = new Product
        {
            name = productDTO.name,
            price = productDTO.price,
            quantity = productDTO.quantity,
            category_id = productDTO.category_id,
            available = productDTO.available,
            image_path = file_path
        };
        await productService.create_product(product);
        return Ok();
    }

    [HttpGet(nameof(GetProducts))]
    public List<Product> GetProducts()
    {
       return productService.GetProducts();
    }
}