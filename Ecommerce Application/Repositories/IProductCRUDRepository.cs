using Ecommerce_Application.Models;

namespace Ecommerce_Application.Repositories
{
    public interface IProductCRUDRepository
    {
        Task<List<ProductDetails>> GetAllDetails();
        Task<List<ProductDetails>> GetLatestProducts();
        Task<ProductDetails?> GetById(int id);
    }
}
