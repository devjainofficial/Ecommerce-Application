using Ecommerce_Application.Models;

namespace Ecommerce_Application.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _context;
        public ProductRepository(DBContext context)
        {
            _context = context;
        }
    }
}
