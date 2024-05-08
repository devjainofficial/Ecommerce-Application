using Ecommerce_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Application.Repositories
{
    public class ProductCRUDRepository : IProductCRUDRepository
    {
        private readonly DBContext _context;
        public ProductCRUDRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDetails>> GetAllDetails()
        {
            return await _context.ProductDetails.Include(p => p.Category).Include(p => p.ProductImages).ToListAsync();
           
        }  
        public async Task<List<ProductDetails>> GetLatestProducts()
        {
            return await _context.ProductDetails.OrderByDescending(p => p.ProductUploadTime).Take(10).Include(p => p.Category).Include(p => p.ProductImages).ToListAsync();
           
        }
        public async Task<ProductDetails?> GetById(int id)
        {
            var product = await _context.ProductDetails
                .FirstOrDefaultAsync(m => m.ProductId == id);
            return product;
        }
    }
}
