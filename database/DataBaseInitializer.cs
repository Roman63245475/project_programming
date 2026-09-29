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
                         DO $$
                         BEGIN
                             IF NOT EXISTS (
                                 SELECT 1
                                 FROM pg_constraint
                                 WHERE conname = 'fk_products_categories'
                             ) THEN
                                 ALTER TABLE products
                                 ADD CONSTRAINT fk_products_categories
                                 FOREIGN KEY (category_id)
                                 REFERENCES categories(id);
                             END IF;
                         END
                         $$;
                         """);
        database.Execute(@"
        ALTER TABLE products
        ADD COLUMN IF NOT EXISTS image_path TEXT;
    ");
    }
}