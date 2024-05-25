using ProductManager.Infrastructures;
using ProductManager.Models;

namespace ProductManager.Applications
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;            
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        } 
        
        public async Task<Product> GetProductAsync(int id)
        {
            return await _repository.GetProductAsync(id);
        }

        public async Task<Product> GetProductAsync(string? name)
        {
            return await _repository.GetProductAsync(name);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            return await _repository.CreateProductAsync(product);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            return await _repository.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteProductAsync(id);
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            return await _repository.DeleteProductAsync(product);
        }

    }
}