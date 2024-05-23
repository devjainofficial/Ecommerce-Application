using Ecommerce_Application.Models;

namespace Ecommerce_Application.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartDetail>> GetAllDetails();
        Task<List<CartDetail>> GetAllDetails(string id);
        void InsertCart(CartDetail cart);
        void UpdateCart(CartDetail cart);
        void SaveChangesAsync();
    }
}
