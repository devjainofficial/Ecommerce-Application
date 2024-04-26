using Ecommerce_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce_Application.Repositories
{
    public interface IUserAuthentication
    {
        Task<Status> LoginAsync(Login login);
        Task LogoutAsync();
        Task<Status> RegisterAsync(Registration register);
        /*Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);*/
        Task<JwtSecurityToken> GenerateToken(Login user);
    }
}
