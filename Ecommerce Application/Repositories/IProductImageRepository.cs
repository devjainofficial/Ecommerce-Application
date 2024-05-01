using Ecommerce_Application.Models;

namespace Ecommerce_Application.Repositories
{
    public interface IProductImageRepository
    {
        Task<List<ProductImage>> GetAllImagesById(int id);
    }
}
