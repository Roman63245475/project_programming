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
        await db.Products.InsertAsync(() => product);
    }
}