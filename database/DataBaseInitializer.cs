using be;
using LinqToDB;
using LinqToDB.Data;

namespace database;

public class DatabaseInitializer
{
    public static void Initialize(DataBase database)
    {
        database.CreateTable<Category>(
            tableOptions: TableOptions.CreateIfNotExists
        );

        database.CreateTable<Product>(
            tableOptions: TableOptions.CreateIfNotExists
        );
        
        database.Execute("""
                         ALTER TABLE products
                         ADD CONSTRAINT FK_products_categories
                         FOREIGN KEY (category_id)
                         REFERENCES categories(id);
                         """);
    }
}