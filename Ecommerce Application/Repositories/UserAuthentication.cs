using Ecommerce_Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce_Application.Repositories
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        public UserAuthentication(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IConfiguration config)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._config = config;
        }

        public async Task<Status> RegisterAsync(Registration register)
        {
            var status = new Status();
            var userExists = await _userManager.FindByNameAsync(register.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already Exist";
                return status;
            }

            User user = new User()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username,
                Name = register.Name,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            if (!await _roleManager.RoleExistsAsync(register.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(register.Role));
            }


            if (await _roleManager.RoleExistsAsync(register.Role))
            {
                await _userManager.AddToRoleAsync(user, register.Role);
            }

            status.StatusCode = 1;
            status.Message = "You have Registered Successfully";
            return status;
        }
        public async Task<Status> LoginAsync(Login login)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user, login.Password))
            {
                status.StatusCode = 0;
                status.Message = "invalid password";
                return status;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in Successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is Locked Out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on Logging in";
            }
            return status;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<JwtSecurityToken> GenerateToken(Login user)
        {


            var userdata = await _userManager.FindByNameAsync(user.Username);


            var signInResult = await _signInManager.PasswordSignInAsync(userdata, user.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(userdata);
                

                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                };
                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:JwtSecretKey"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                    _config["Jwt:JWtIssuer"],
                    _config["Jwt:JwtAudience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                return token; 
            }
            return null;
        }
    }
}

