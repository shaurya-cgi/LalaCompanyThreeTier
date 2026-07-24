using LalaCompanyThreeTier.Data;
using LalaCompanyThreeTier.Interfaces;
using LalaCompanyThreeTier.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace LalaCompanyThreeTier.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;

        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _appDbContext.Products.ToListAsync();
        }
        public async Task<Product?> GetProductById(int id)
        {
            return await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByCategory(int CategoryId)
        {
            return await _appDbContext.Products.Where(p => p.CategoryId == CategoryId).ToListAsync();
        }
        public async Task<Product?> CreateProduct(Product product)
        {
            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }
        public async Task<Product?> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProduct != null)
            {
                if (product != null)
                {
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.ModelName = product.ModelName;
                    existingProduct.DefaultPrice = product.DefaultPrice;
                    existingProduct.Gstrate = product.Gstrate;
                    existingProduct.UpdatedAt = DateTime.UtcNow;

                    _appDbContext.Products.Update(existingProduct);
                    await _appDbContext.SaveChangesAsync();
                    return existingProduct;
                }
                return product;
            }
            return null;
        }
        public async Task<Product?> DeleteProduct(int id)
        {
            var existingProduct = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProduct != null)
            {
                _appDbContext.Products.Remove(existingProduct);
                await _appDbContext.SaveChangesAsync();
                return null;
            }
            return null;
        }   
    }
}
