using be;
// using Facet;

namespace api;

public class ProductDTO
{
    public int ?id { get; set; }
    public string name { get; set; }
    public decimal price { get; set; }
    public int quantity { get; set; }
    public bool available { get; set; }
    public int category_id { get; set; }
    public IFormFile? image { get; set; }

    public ProductDTO()
    {
    }
    
    
}