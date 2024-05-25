using ProductManager.Models;

namespace ProductManager.Infrastructures
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> GetProductAsync(string? name);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> DeleteProductAsync(Product product);
    }
}