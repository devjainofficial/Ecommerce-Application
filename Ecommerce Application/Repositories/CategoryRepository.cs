using Ecommerce_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Application.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DBContext _context;
        public CategoryRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDetails>> GetAllProductsByCategoriesAsync(int id)
        {
            var product = await _context.ProductDetails.Where(c => c.CategoryId == id).ToListAsync();
            return product;
        }
        public async Task<List<ProductDetails>> GetAllProductsAsync(int id)
        {
            var product = await _context.ProductDetails.ToListAsync();
            return product;
        }
        public async Task<List<Category>> GetAllCategoriesAsync(int id)
        {
            var category =  await _context.Categories.ToListAsync();
            
            return category;
        }
         public async Task<List<Category>> GetCategoriesDetailByIdAsync(int id)
        {
            return await _context.Categories.Where(c => c.ParentId == id).ToListAsync();
        }
        


       /* List<ProductDetails> product = new List<ProductDetails>();
        public async Task<List<ProductDetails>> GetAllProductsByCategoriesAsync(int[] categoryId)
        {
            foreach(var category in categoryId)
            {

            product = await _context.ProductDetails.Where(c => c.CategoryId == category).ToListAsync();
            }
            return product;
        }*/
    }
}
