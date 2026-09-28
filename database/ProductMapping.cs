namespace database;

using LinqToDB.Mapping;
using be;

public static class ProductMapping
{
    public static void Configure(FluentMappingBuilder builder)
    {
        builder.Entity<Product>()
            .HasTableName("products")
            .HasPrimaryKey(x => x.id);

        builder.Entity<Product>()
            .Property(x => x.id)
            .IsIdentity();

        builder.Entity<Product>()
            .Property(x => x.name)
            .HasColumnName("product_name");

        builder.Entity<Product>()
            .Property(x => x.price)
            .HasColumnName("price");

        builder.Entity<Product>()
            .Property(x => x.quantity)
            .HasColumnName("quantity");

        builder.Entity<Product>()
            .Property(x => x.available)
            .HasColumnName("available");

        builder.Entity<Product>()
            .Property(x => x.category_id)
            .HasColumnName("category_id");
    }
}