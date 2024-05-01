using Ecommerce_Application.Models;

namespace Ecommerce_Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category?> GetById(int id);
        void DeleteCategory(int CategoryId);
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void SaveChangesAsync();
        bool CategoryExists(int id);
    }
}
