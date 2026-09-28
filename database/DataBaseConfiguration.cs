namespace database;

using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using LinqToDB.Mapping;

public static class DatabaseConfiguration
{
    public static DataBase Create(string connectionString)
    {
        var mappingSchema = new MappingSchema();

        var builder = new FluentMappingBuilder(mappingSchema);

        CategoryMapping.Configure(builder);
        ProductMapping.Configure(builder);

        builder.Build();

        var options = new DataOptions()
            .UsePostgreSQL(
                connectionString,
                PostgreSQLVersion.AutoDetect)
            .UseMappingSchema(mappingSchema);

        var typedOptions = new DataOptions<DataBase>(options);

        return new DataBase(typedOptions);
    }
}