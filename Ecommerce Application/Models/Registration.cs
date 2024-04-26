using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Application.Models
{
    public class Registration
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string PasswordConfirm {  get; set; }
        public string Role { get; set; }
    }
}
