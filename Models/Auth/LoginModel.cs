using System.ComponentModel.DataAnnotations;

namespace ASPNetCore6Identity.Models.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        //[EmailAddress(ErrorMessage = "Por favor introduzca un email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
