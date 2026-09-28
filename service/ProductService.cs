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
        await db.Products.InsertAsync(() => new Product()
        {
            name = product.name,
            price = product.price,
            quantity = product.quantity,
            available =  product.available,
            category_id =  product.category_id
        });
    }
}