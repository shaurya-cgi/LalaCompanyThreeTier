using LalaCompanyThreeTier.Models;

namespace LalaCompanyThreeTier.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<List<Product>> GetProductsByCategory(int categoryid);
        Task<Product?> CreateProduct(Product product);
        Task<Product?> UpdateProduct(int id, Product product);
        Task<Product?> DeleteProduct(int id);
    }
}
