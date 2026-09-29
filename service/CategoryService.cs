using api;
using be;
using database;
using LinqToDB;
using Npgsql;

namespace service;

public class CategoryService(DataBase db) {
    public async Task<(bool isSuccess, string message)> CreateCategory(CategoryDTO categoryDTO) {
        try {
            var category = new Category {
                name = categoryDTO.name
            };
            await db.InsertAsync(category);
            return (true, "Successfully created category!");
        }
        catch (PostgresException ex) when (ex.SqlState == "23505") {
            return (false, "Couldn't create category! The category already exists.");
        }
    }
}