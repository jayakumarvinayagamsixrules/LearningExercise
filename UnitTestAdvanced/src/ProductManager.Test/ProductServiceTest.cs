using FluentAssertions;
using ProductManager.Models;
using ProductManager.Infrastructures;
using ProductManager.Applications;
using FakeItEasy;

namespace ProductManager.Test;

public class ProductServiceTest
{
    public ProductServiceTest()
    {
        
    }
    [Fact]
    [Trait("GetProductsAsync","Unit-ProductService")]
    public async Task GetProducts_With_Initial_ShouldReturnEmptyCollection()
    {
        // Arrange
        var mockProductRepository = A.Fake<IProductRepository>(); //new Mock<IProductRepository>();
        var mockProducts = A.CollectionOfFake<Product>(0);
        A.CallTo(() => mockProductRepository.GetAllProductsAsync()).Returns(Task.FromResult(mockProducts.AsEnumerable()));

        // Act
        var mockProductService = new ProductService(mockProductRepository);

        // Assert
        var actualProducts = await mockProductService.GetProductsAsync();  
        actualProducts.Should()
            .NotBeNull()
            .And
            .BeEmpty()
            .And
            .HaveCount(0);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(25)]
    [Trait("GetProductsAsync","Unit-ProductService")]
    public async Task GetProducts_ShouldReturnCollection(int itemCount)
    {
        // Arrange
        var mockProductRepository = A.Fake<IProductRepository>(); //new Mock<IProductRepository>();
        var mockProducts = A.CollectionOfFake<Product>(itemCount);
        A.CallTo(() => mockProductRepository.GetAllProductsAsync()).Returns(Task.FromResult(mockProducts.AsEnumerable()));

        // Act
        var mockProductService = new ProductService(mockProductRepository);

        // Assert
        var actualProducts = await mockProductService.GetProductsAsync();  
        actualProducts.Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            .And
            .HaveCount(itemCount);
    }
        
    [Fact]
    [Trait("GetProductsAsync by Id","Unit-ProductService")]
    public async Task GetProduct_WithAnInvalidId_ShouldReturnNull()
    {
        // Arrange
        var mockProductRepository = A.Fake<IProductRepository>(); //new Mock<IProductRepository>();
        Product mockProduct = null;
        A.CallTo(() => mockProductRepository.GetProductAsync(-10)).Returns(Task.FromResult(mockProduct));

        // Act
         var mockProductService = new ProductService(mockProductRepository);
        
        // Assert
        var actualProducts = await mockProductService.GetProductAsync(-10); 
        actualProducts.Should().BeNull();
            
    }

    [Theory]
    [InlineData(101)]
    [InlineData(5001)]
    [InlineData(123078)]
    [Trait("GetProductsAsync by Id","Unit-ProductService")]
    public async Task GetProduct_WithAValidId_ShouldReturnProduct(int id)
    {
        // Arrange
        var mockProductRepository = A.Fake<IProductRepository>(); //new Mock<IProductRepository>();
        var mockProduct = A.Fake<Product>();
        mockProduct.Id = id;
        A.CallTo(() => mockProductRepository.GetProductAsync(id)).Returns(Task.FromResult(mockProduct));

        // Act
         var mockProductService = new ProductService(mockProductRepository);
        
        // Assert
        var actualProducts = await mockProductService.GetProductAsync(id); 
        actualProducts.Should()
            .NotBeNull()
            .And
            .Match(d => ((d! as Product)!.Id == id));
    }

    [Theory]
    [InlineData("Dell Laptop")]
    [InlineData("Shimla Apple")]
    [InlineData("Sony-Headset")]
    [Trait("GetProductAsync by Name","Unit-ProductService")]
    public async Task GetProduct_WithAnValidName_ShouldReturnProduct(string name)
    {
        // Arrange
        var mockProductRepository = A.Fake<IProductRepository>(); //new Mock<IProductRepository>();
        var mockProduct = A.Fake<Product>();
        mockProduct.Name = name;
        A.CallTo(() => mockProductRepository.GetProductAsync(name)).Returns(Task.FromResult(mockProduct));

        // Act
         var mockProductService = new ProductService(mockProductRepository);
        
        // Assert
        var actualProduct = await mockProductService.GetProductAsync(name); 
        actualProduct.Should()
            .NotBeNull()
            .And
            .Match(d => ((d as Product)!.Name == name));
    }

    [Fact]
    [Trait("GetProductAsync by Name","Unit-ProductService")]
    public async Task GetProduct_WithAnInvalidName_ShouldReturnNull()
    {
        // Arrange
        var mockProductRepository = A.Fake<IProductRepository>(); 
       Product mockProduct = null;
        string? productName = "";
        A.CallTo(() => mockProductRepository.GetProductAsync(productName)).Returns(Task.FromResult(mockProduct));

        // Act
         var mockProductService = new ProductService(mockProductRepository);
        
        // Assert
        var actualProduct = await mockProductService.GetProductAsync(productName); 
        actualProduct.Should().BeNull();
    }
}
