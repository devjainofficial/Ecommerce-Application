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
        public async Task<List<Category>> GetAllCategoriesAsync(int id)
        {
            return await _context.Categories.ToListAsync();
        }


    }
}
