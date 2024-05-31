using Ecommerce_Application.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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

        public async Task<List<CartDetail>> GetAllDetails()
        {
            var productDetails = await _context.ProductDetails.Include(p => p.Category).Include(p => p.ProductImages).ToListAsync();
            var cartDetails = await _context.CartDetails.Include(c => c.ProductDetails).ToListAsync();
            return cartDetails;
        }

        public async Task<List<CartDetail>> GetAllDetails(string id)
        {
            var productDetails = await _context.ProductDetails.Include(p => p.Category).Include(p => p.ProductImages).ToListAsync();
            var cartDetails = await _context.CartDetails
                .Include(c => c.ProductDetails)
                .Where(c => c.Id == id)
                .ToListAsync();

            return cartDetails;
        }

        public void InsertCart(CartDetail cart)
        {
            _context.CartDetails.Add(cart);
        }
        public void UpdateCart(CartDetail cart)
        {
           
            _context.Update(cart);
           
        }

        public void DeleteProductIdFromCart(int productId)
        {
            CartDetail productRemove = _context.CartDetails.FirstOrDefault(x => x.ProductId == productId);
            _context.CartDetails.Remove(productRemove);

        }
        public void DeleteCart(CartDetail cart)
        {
            var cartId = cart.CartId;
            CartDetail cartRemove = _context.CartDetails.Find(cartId);
            _context.CartDetails.Remove(cartRemove);
        }
        public void SaveChangesAsync()
        {
            _context.SaveChanges();
        }
    }
}

