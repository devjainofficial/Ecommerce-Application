using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Application.Models
{
    public class User : IdentityUser
    {
 
        public string Name {  get; set; }
        public string Email { get; set; }
       
    }
}
