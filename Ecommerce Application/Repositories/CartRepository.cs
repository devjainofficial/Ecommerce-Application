using Ecommerce_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Application.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DBContext _context;
        public CartRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<CartItems>> GetAllItems(int userId)
        {
            return await _context.CartItems.ToListAsync();
        }

    }
}
