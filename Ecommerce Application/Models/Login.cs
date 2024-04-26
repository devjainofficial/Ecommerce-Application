using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Ecommerce_Application.Models
{
    public class Login
    {
        [Required]
        public string? Username {get; set;}
        [Required] 
        public string? Password { get; set;}
    }
}
