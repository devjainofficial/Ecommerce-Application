using Ecommerce_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Application.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductDetails>> GetAllDetails();
        Task<List<ProductDetails>> GetLatestProducts();
        Task<ProductDetails> GetById(int id);
    }
}
