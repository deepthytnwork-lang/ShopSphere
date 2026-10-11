using Moq;
using ShopSphere.Application.Abstractions;
using ShopSphere.Application.Products;
using ShopSphere.Domain;

namespace ShopSphere.Tests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repo = new();
    private readonly ProductService _sut;

    public ProductServiceTests() => _sut = new ProductService(_repo.Object);

    [Fact]
    public async Task CreateAsync_ValidRequest_AddsProductAndSaves()
    {
        var result = await _sut.CreateAsync(new ProductRequest("Laptop", "15 inch", 55000m, 10));

        Assert.Equal("Laptop", result.Name);
        _repo.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
        _repo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ProductMissing_ReturnsNull()
    {
        _repo.Setup(r => r.GetbyIdAsync(99, It.IsAny<CancellationToken>())).ReturnsAsync((Product?)null);

        var result = await _sut.GetByIDAsync(99);

        Assert.Null(result);
    }

    [Fact]
    public void Product_NegativePrice_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Product("Pen", -5m, 1));
    }
}