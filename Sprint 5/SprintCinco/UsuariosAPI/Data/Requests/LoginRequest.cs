using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
