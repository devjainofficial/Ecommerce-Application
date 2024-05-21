using Ecommerce_Application.Models;

namespace Ecommerce_Application.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItems>> GetAllItems(int userId);
    }
}
