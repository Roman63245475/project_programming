using be;
using database;
using LinqToDB;

namespace service;

public class CategoryService(DataBase db) {
    public async Task CreateCategory(Category category) { 
        await db.InsertAsync(category);
    }
}