using System.ComponentModel.DataAnnotations;
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

    public ProductService()
    {
        
    }

    public bool validateData(Product product)
    {
        if (product is not null)
        {
            switch (product)
            {
                case Product p when p.name.Trim().Length == 0:
                    throw new ValidationException("Name is required");
                    break;
                case Product p when p.price < 0:
                    throw new ValidationException("Price can't be less than zero");
                    break;
                case Product p when p.quantity < 0:
                    throw new ValidationException("Quantity can't be less than zero");
                    break;
                case Product p when p.category_id == null:
                    throw  new ValidationException("Category id required");
                    break;
                case Product p when p.image_path == null:
                    throw new ValidationException("Image is required");
                default:
                    return true;
            }   
        }
        else
        {
            throw new ValidationException("No data has been recieved");
        }
    }

    public async Task create_product(Product product)
    {
        if (validateData(product))
        {
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
    }

    public List<Product> GetProductsByCategoryId(CategoryDTO categoryDTO) {
        return db.Products.Where(p => p.category_id == categoryDTO.id).ToList();
    }

    public async Task<List<Product>> GetProducts()
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
    
    public async Task Delete(int id) {
        await db.Products.Where((p) => p.id == id).DeleteAsync();
    }
}