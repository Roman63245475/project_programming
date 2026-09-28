namespace database;

using LinqToDB.Mapping;
using be;


public static class CategoryMapping
{
    public static void Configure(FluentMappingBuilder builder)
    {
        builder.Entity<Category>()
            .HasTableName("categories")
            .HasPrimaryKey(x => x.id);

        builder.Entity<Category>()
            .Property(x => x.id)
            .IsIdentity();

        builder.Entity<Category>()
            .Property(x => x.name)
            .HasColumnName("category_name");
    }
}