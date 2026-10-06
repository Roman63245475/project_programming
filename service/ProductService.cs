using api;
using be;
using database;
using LinqToDB;
namespace service;

public class ProductService
{
    public DataBase db;

    public ProductService(DataBase db)
    {
        this.db = db;
    }

    public async Task create_product(Product product)
    {
        Console.WriteLine($"db is null: {db == null}");
        Console.WriteLine($"product is null: {product == null}");
        Console.WriteLine($"name: {product.name}");
        Console.WriteLine($"category_id: {product.category_id}");
        Console.WriteLine($"image_path: {product.image_path}");
        await db.Products.InsertAsync(() => new Product()
        {
            name = product.name,
            price = product.price,
            quantity = product.quantity,
            available =  product.available,
            category_id =  product.category_id,
            image_path = product.image_path
        });
    }

    public List<Product> GetProductsByCategoryId(CategoryDTO categoryDTO) {
        return db.Products.Where(p => p.category_id == categoryDTO.id).ToList();
    }

    public List<Product> GetProducts()
    {
        return db.Products.ToList();
    }

    public Product GetProduct(int id)
    {
        
       return db.Products.Where(product => product.id == id).FirstOrDefault() ?? throw new Exception("Product not found");
        
       }

    public async Task purchase(int id, int purchase_quantity)
    {
        await db.Products.Where(p => p.id == id).Set(p => p.quantity, p => p.quantity-purchase_quantity).UpdateAsync();
    }

    public async Task fbi_caught(int id)
    {
        await db.Products.Where(p => p.id == id).DeleteAsync();
    }
}
    
    