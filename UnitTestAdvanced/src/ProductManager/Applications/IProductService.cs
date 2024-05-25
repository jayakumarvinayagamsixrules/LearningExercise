using ProductManager.Models;

namespace ProductManager.Applications
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> GetProductAsync(string? name);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> DeleteProductAsync(Product product);

    }
}
