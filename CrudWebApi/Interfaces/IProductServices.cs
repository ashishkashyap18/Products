using CrudWebApi.Models;

namespace CrudWebApi.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<Product?>> GetProducts();
        Task<Product?> GetProductById(int id);
        Task<Product?> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}
