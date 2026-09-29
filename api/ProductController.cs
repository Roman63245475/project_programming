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
    public async Task<IActionResult> create_product([FromBody] Product product)
    {
        await productService.create_product(product);
        return Ok();
    }

    [HttpGet(nameof(GetProducts))]
    public List<Product> GetProducts()
    {
       return productService.GetProducts();
    }
}