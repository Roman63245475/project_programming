using be;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB;
namespace database;

public class DataBase : DataConnection
{

    public DataBase(DataOptions<DataBase> options) : base(options.Options)
    {
        
    }

    public ITable<Product> Products => this.GetTable<Product>();

    public ITable<Category> Categories => this.GetTable<Category>();
}
