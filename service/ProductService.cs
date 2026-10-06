using api;
using be;
using database;
using LinqToDB;
using LinqToDB.Async;

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

    public async Task<List<Product>> GetProducts(CategoryDTO categoryDTO) {
        return db.Products.Where(p => p.category_id == categoryDTO.id).ToList();
    }
    
    public async Task Delete(int id) {
        await db.Products.Where((p) => p.id == id).DeleteAsync();
    }

    public async Task<Product> GetProduct(int id) {
        return await db.Products.FirstOrDefaultAsync(p => p.id == id);
    }

    public async Task Update(Product product, int id) {
        var query = db.Products.Where(p => p.id == id);
        if (!string.IsNullOrEmpty(product.image_path)) {
            await query.Set(p => p.name, product.name)
                .Set(p => p.price, product.price)
                .Set(p => p.quantity, product.quantity)
                .Set(p => p.category_id, product.category_id)
                .Set(p => p.available, product.available)
                .Set(p => p.image_path, product.image_path).UpdateAsync();
        }else {
            await query.Set(p => p.name, product.name)
                .Set(p => p.price, product.price)
                .Set(p => p.quantity, product.quantity)
                .Set(p => p.category_id, product.category_id)
                .Set(p => p.available, product.available).UpdateAsync();
        }
    }
}