using Ecommerce_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Application.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DBContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        public CategoryRepository(DBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
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

        public List<Banner> BannerImage(string name)
        {
            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "img/"+name));

            //Copy File names to Model collection.
            List<Banner> files = new List<Banner>();
            foreach (string filePath in filePaths)
            {
                files.Add(new Banner { FileName = Path.GetFileName(filePath) });
            }

            return files;
        }

    }
}
