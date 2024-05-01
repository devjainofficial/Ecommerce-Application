using Ecommerce_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Application.Repositories
{
    public class ProductImageRepository: IProductImageRepository
    {
        private readonly DBContext _context;
        public ProductImageRepository(DBContext context)
        {
            _context = context;
        }

   
        public async Task<List<ProductImage>> GetAllImagesById(int id)
        {
            var productImages = await _context.ProductImages.Where(pi => pi.ProductId == id).ToListAsync();
            return productImages;
        }
       

    }
}
