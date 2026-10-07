using System.ComponentModel.DataAnnotations;
using be;
using service;

namespace unit_tests;

public class Tests
{

    private Product product;
    private ProductService productService;
    [SetUp]
    public void Setup()
    {
        this.product = new Product(1, "", -1, -1, true, 1, "img_path");
        this.productService = new ProductService();
    }

    [Test]
    public void checkName()
    {
        var exception = Assert.Throws<ValidationException>(() =>
        {
            this.productService.validateData(product);
        });

        Assert.That(exception.Message, Is.EqualTo("Name is required"));
    }
    
    
    [Test]
    public void checkPrice()
    {
        this.product.name = "name";
        var exception = Assert.Throws<ValidationException>(() =>
        {
            this.productService.validateData(product);
        });

        Assert.That(exception.Message, Is.EqualTo("Price can't be less than zero"));
    }
    
    [Test]
    public void checkQuantity()
    {
        this.product.name = "name";
        this.product.price = 1;
        var exception = Assert.Throws<ValidationException>(() =>
        {
            this.productService.validateData(product);
        });

        Assert.That(exception.Message, Is.EqualTo("Quantity can't be less than zero"));
    }
    
    
    
    [Test]
    public void check_All()
    {
        
        this.product.name = "name";
        this.product.price = 1;
        this.product.quantity = 1;

        Assert.That(this.productService.validateData(product), Is.True);
    }
}   