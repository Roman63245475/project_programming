namespace be;

public class Product
{
    public int ?id { get; set; }
    public string name { get; set; }
    public decimal price { get; set; }
    public int quantity { get; set; }
    public bool available { get; set; }
    public int category_id { get; set; }
    

    public Product(int id, string name, decimal price, int quantity, bool available, int category_id)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.quantity = quantity;
        this.available = available;
        this.category_id = category_id;
    }

    public Product()
    {
    }


}