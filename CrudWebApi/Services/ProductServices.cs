using CrudWebApi.AppDbContext;
using CrudWebApi.Interfaces;
using CrudWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudWebApi.Services
{
    public class ProductServices : IProductServices
    {
        private readonly AppDbContent _content;

        public ProductServices(AppDbContent content)
        {
            _content = content;
        }

        public async Task<Product?> CreateProduct(Product product)
        {
            _content.Products.Add(product);
            await _content.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await GetProductById(id);
            if (product == null)
            {
                return false;
            }
            _content.Products.Remove(product);
            await _content.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> GetProductById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid product Id", nameof(id));
            }
            var product = await _content.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }

        public async Task<IEnumerable<Product?>> GetProducts()
        {
           return await _content.Products.ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _content.Entry(product).State = EntityState.Modified;
            var rowAffected = await _content.SaveChangesAsync();
            if(rowAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
