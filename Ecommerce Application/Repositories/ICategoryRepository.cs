using Ecommerce_Application.Models;

namespace Ecommerce_Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<ProductDetails>> GetAllProductsByCategoriesAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync(int id);
    }
}
